using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gama.Application.Seedworks.ValidationContracts
{
    public class RequestValidationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                actionArgument.Deconstruct(out var key, out var value);
                var request = value as IRequest;

                if (request is null)
                {
                    continue;
                }
                
                request.Validate();
                var notifiable = request as Notifiable<Notification>;
                if (notifiable is null)
                {
                    continue;
                }

                if (notifiable.IsValid)
                {
                    continue;
                }

                foreach (var notification in notifiable.Notifications)
                {
                    context.ModelState.AddModelError(key + '.' + notification.Key, notification.Message);
                }
            }

            await next();
        }
    }
}
