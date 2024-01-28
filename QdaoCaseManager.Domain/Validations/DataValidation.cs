namespace QdaoCaseManager.Domain.Validations;
public static class DataValidation
{
    public static void GuardAgainstNullString(string? value, string? parameterName = null, string message = "The '{parameterName}' parameter cannot be null.")
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }
    }
   
}
