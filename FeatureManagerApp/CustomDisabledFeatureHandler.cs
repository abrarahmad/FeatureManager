using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureManagerApp
{
    public class CustomDisabledFeatureHandler : IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {

            var message = $"The folloing feattures are not aviable at this time :{string.Join(',', features)}";
            context.Result = new ContentResult
            {
                ContentType = "text/plain",
                Content = message,
                StatusCode = 404
            };
            return Task.CompletedTask;
        }
    }
}
