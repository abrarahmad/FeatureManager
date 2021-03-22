using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace FeatureManagerApp
{
    [FilterAlias("RandomFeature")]
    //[FilterAlias("Feature.FlagApp.RandomFeature")]//with namespace
    public class RandomFilter : IFeatureFilter
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            RandomFilterSettings settings = context.Parameters.Get<RandomFilterSettings>();
            if (settings.Method == "Even")
            {
                return Task.FromResult(DateTime.Now.Ticks % 2 == 0);
            }
            if (settings.Method == "Odd")
            {
                return Task.FromResult(DateTime.Now.Ticks % 2 != 0);
            }

            throw new Exception($"Random feature filter configured value '{settings.Method}' is invalid");
        }
    }
}
