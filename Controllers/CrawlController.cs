using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppTest.Factories;
using WebAppTest.Crawlers;

namespace WebAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrawlController : ControllerBase
    {
        private readonly ICrawlerFactory _crawlerFactory;
        private readonly ILogger<CrawlController> _logger;

        public CrawlController(ICrawlerFactory crawlerFactory, ILogger<CrawlController> logger)
        {
            _crawlerFactory = crawlerFactory;
            _logger = logger;
        }

        [HttpGet("{type}")]
        public IActionResult Get(string type)
        {
            _logger.LogInformation("CrawlController.Get called with type: {Type}", type);

            var crawler = _crawlerFactory.CreateCrawler(type);
            if (crawler == null)
            {
                _logger.LogWarning("Crawler not found for type: {Type}", type);
                return NotFound();
            }

            var result = crawler.Crawl();
            _logger.LogInformation("Crawler result: {Result}", result);
            return Ok(result);
        }

        // Default route
        [HttpGet]
        public IActionResult GetDefault()
        {
            _logger.LogInformation("CrawlController.GetDefault called");

            var crawler = _crawlerFactory.CreateCrawler("kaki");
            var result = crawler.Crawl();
            _logger.LogInformation("Crawler result: {Result}", result);
            return Ok(result);
        }
    }
}