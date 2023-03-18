namespace Gama.Domain.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }
    
    public ValidationException(ValidationError validationError)
    {
        Errors = new[] { validationError };
    }

    public ValidationException(IEnumerable<ValidationError> validationErrors)
    {
        Errors = validationErrors;
    }
}

public class ValidationError
{
    public string PropertyName { get; init; }

    public string ErrorMessage { get; init; }   
}