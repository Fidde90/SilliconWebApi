using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SilliconWebApi.Filters
{
    [AttributeUsage(validOn:AttributeTargets.Class | AttributeTargets.Method)]
    public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //hämtar in appsettings.json filen
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = config.GetValue<string>("ApiKey"); // hämtar upp nyckeln

            if(!context.HttpContext.Request.Query.TryGetValue("key", out var providedKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey!.Equals(providedKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
