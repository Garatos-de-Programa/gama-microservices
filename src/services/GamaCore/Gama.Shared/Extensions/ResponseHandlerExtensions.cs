using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Shared.Extensions;

public static class ResponseHandlerExtensions
{
    public static IActionResult ToBadRequest<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(s => new StatusCodeResult(500), exception =>
        {
            if (exception is ValidationException validationException)
            {
                return new BadRequestObjectResult(validationException.ToProblemDetails());
            }

            return new StatusCodeResult(500);
        });
    }
}