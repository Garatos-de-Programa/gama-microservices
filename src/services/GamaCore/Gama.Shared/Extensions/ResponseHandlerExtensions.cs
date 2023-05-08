using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gama.Shared.Extensions;

public static class ResponseHandlerExtensions
{
    public static IActionResult ToBadRequest<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(s => new StatusCodeResult(500), FailHandler);
    }

    public static IActionResult ToOk<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(
            success =>
            {
                var response = mapper(success);
                return new OkObjectResult(response);
            },
            FailHandler
        );
    }
    
    public static IActionResult ToOk<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(
            success => new OkObjectResult(success),
            FailHandler
        );
    }

    public static IActionResult ToNoContent<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(
            success => new NoContentResult(),
            FailHandler
        );
    }

    public static IActionResult ToCreated<TResult>(this Result<TResult> result)
    {
        return result.Match<IActionResult>(
            success => new StatusCodeResult((int)HttpStatusCode.Created),
            FailHandler
        );
    }


    internal static IActionResult FailHandler(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return new BadRequestObjectResult(validationException.ToProblemDetails());
        }

        return new StatusCodeResult(500);
    }
}