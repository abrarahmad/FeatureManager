using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeatureManagerApp
{
    public class SlowDownActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Task.Delay(5000);
            await next();
        }
    }
}
