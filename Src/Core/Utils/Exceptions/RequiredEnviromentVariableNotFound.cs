namespace Core.Utils.Exceptions;

internal class RequiredEnviromentVariableNotFound : Exception
{
    const string MESSAGE_TEMPLATE = "Enviroment Variable not found, variable Name: {0}";

    internal RequiredEnviromentVariableNotFound(string key) : base(string.Format(MESSAGE_TEMPLATE, key)) { }

    public static void ThrowIfNullOrWhiteSpace(string? value, string key)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new RequiredEnviromentVariableNotFound(key);
        }
    }
}
