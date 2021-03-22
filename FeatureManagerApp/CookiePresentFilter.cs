using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace FeatureManagerApp
{
    public class CookiePresentFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookiePresentFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.Get<CookiePresentFilterSettings>();
            bool isEnabled = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(settings.CookieName);
            return Task.FromResult(isEnabled);
        }
    }
}
