using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Collabra_Test.Models;

namespace Collabra_Test.Controllers
{
    [Route("api/HackerNews")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        [HttpGet]
        [Route("GetBestStories")]
        public async Task<List<HackerNewsStory>> GetBestStories(int storyCount = 10)
        {
            return
                await new Services.HackerNewsApi().GetBestStories(storyCount);
        }
    }
}

