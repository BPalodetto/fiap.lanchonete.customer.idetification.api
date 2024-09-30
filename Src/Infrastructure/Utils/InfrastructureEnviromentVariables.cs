using Infrastructure.Utils.Exceptions;

namespace Infrastructure.Utils;

public static class InfrastructureEnviromentVariables
{
    static InfrastructureEnviromentVariables()
    {
        FiapLanchonetApi = GetRequiredEnviromentVariable("FIAP_LANCHONET_API_URI");
    }
    public static string FiapLanchonetApi { get; }
    static string GetRequiredEnviromentVariable(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);
        InfrastructureRequiredEnviromentVariableNotFound.ThrowIfNullOrWhiteSpace(value, key);
        return value!;
    }
}
