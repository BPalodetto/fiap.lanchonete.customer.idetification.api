using Core.Utils.Exceptions;

namespace Core.Utils;

public static class CoreEnviromentVariables
{
    static CoreEnviromentVariables()
    {
        AuthKey = GetRequiredEnviromentVariable("AUTH_KEY");
        FiapLanchonetApi = GetRequiredEnviromentVariable("FIAP_LANCHONET_API_URI");
    }
    public static string AuthKey { get; }
    public static string FiapLanchonetApi { get; }
    static string GetRequiredEnviromentVariable(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);
        RequiredEnviromentVariableNotFound.ThrowIfNullOrWhiteSpace(value, key);
        return value!;
    }
}
