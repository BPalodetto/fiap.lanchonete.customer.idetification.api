namespace Infrastructure.Utils.Exceptions;

internal class InfrastructureRequiredEnviromentVariableNotFound : Exception
{
    const string MESSAGE_TEMPLATE = "Enviroment Variable not found, variable Name: {0}";

    internal InfrastructureRequiredEnviromentVariableNotFound(string key) : base(string.Format(MESSAGE_TEMPLATE, key)) { }

    public static void ThrowIfNullOrWhiteSpace(string? value, string key)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InfrastructureRequiredEnviromentVariableNotFound(key);
        }
    }
}
