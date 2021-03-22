using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;

namespace FeatureManagerApp
{
    public class BetaCookieFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BetaCookieFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            bool isEnabled = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("beta");
            return Task.FromResult(isEnabled);
        }
    }
}
