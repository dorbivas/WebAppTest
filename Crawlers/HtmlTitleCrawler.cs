using System.Net.Http;
using System.Threading.Tasks;

namespace WebAppTest.Crawlers
{
    public class HtmlTitleCrawler : ICrawler
    {
        public string Crawl()
        {
            // Simplified example, in a real scenario you would use HttpClient and parse HTML
            return "<title>Example Title</title>";
        }
    }
}