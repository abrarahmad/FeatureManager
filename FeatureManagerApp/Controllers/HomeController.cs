using System.Diagnostics;
using System.Threading.Tasks;
using FeatureManagerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureManagerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IFeatureManager _featureManager;
        private readonly IFeatureManagerSnapshot _featureManagerSnapshot; // to maintain consistency of requests 
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ILogger<HomeController> logger,
            //IFeatureManager featureManager,
            IFeatureManagerSnapshot featureManagerSnapshot,
            IHttpContextAccessor contextAccessor
            )
        {
            _logger = logger;
            //_featureManager = featureManager;
            _featureManagerSnapshot = featureManagerSnapshot;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> IndexAsync()
        {
            //if (await _featureManager.IsEnabledAsync(nameof(FeatureFlags.Printing)))
            //{
            //    ViewData["PrintingMessage"] = "On";
            //}
            //else
            //{
            //    ViewData["PrintingMessage"] = "Off";
            //}
            for (int i = 0; i < 10; i++)
            {
                //ViewData["PrintingMessage"] += (await _featureManager.IsEnabledAsync(nameof(FeatureFlags.Printing))).ToString() + ",";
                //ViewData["PrintingMessage"] += (await _featureManagerSnapshot.IsEnabledAsync(nameof(FeatureFlags.Printing))).ToString() + ",";
                //await Task.Delay(2000);
                ViewData["PrintingMessage"] += (await _featureManagerSnapshot.IsEnabledAsync(nameof(FeatureFlags.Printing))).ToString() + ",";
            }
            _contextAccessor.HttpContext.Session.SetInt32("set something in session to ratain across requests", 42);
            ViewData["SessionId"] = _contextAccessor.HttpContext.Session.Id;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[FeatureGate(RequirementType.Any, nameof(FeatureFlags.Printing), nameof(FeatureFlags.PrintPreview))]
        [FeatureGate(nameof(FeatureFlags.Printing))]
        public IActionResult Print()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
