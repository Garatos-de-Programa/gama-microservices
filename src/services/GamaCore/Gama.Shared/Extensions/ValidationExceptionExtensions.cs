using Gama.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Shared.Extensions;

public static class ValidationExceptionExtensions
{
    public static ValidationProblemDetails ToProblemDetails(this ValidationException exception)
    {
        var error = new ValidationProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Status = 400
        };

        foreach (var validationError in exception.Errors)
        {
            if (error.Errors.ContainsKey(validationError.PropertyName))
            {
                error.Errors[validationError.PropertyName] =
                    error.Errors[validationError.PropertyName]
                        .Concat(new[] { validationError.ErrorMessage }).ToArray();
                
                continue;
            }

            error.Errors.Add(new KeyValuePair<string, string[]>(
                validationError.PropertyName,
                new[] { validationError.ErrorMessage }
            ));
        }
        
        return error;
    }
}