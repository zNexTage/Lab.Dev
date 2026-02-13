using System.Diagnostics;
using Lab.CustomProgram.Configuration;
using Lab.CustomProgram.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab.CustomProgram.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApiConfiguration _apiConfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptions<ApiConfiguration> apiConfiguration)
        {
            _logger = logger;
            _configuration = configuration;
            _apiConfiguration = apiConfiguration.Value;
        }

        public IActionResult Index()
        {
            var apiConfig = new ApiConfiguration();

            _configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);

            return View();
        }

        public IActionResult Privacy()
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
