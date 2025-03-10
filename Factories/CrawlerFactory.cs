using WebAppTest.Crawlers;

namespace WebAppTest.Factories
{
    public interface ICrawlerFactory
    {
        ICrawler CreateCrawler(string type);
    }

    public class CrawlerFactory : ICrawlerFactory
    {
        public ICrawler CreateCrawler(string type)
        {
            return type.ToLower() switch
            {
                "helloworld" => new HelloWorldCrawler(),
                "htmltitle" => new HtmlTitleCrawler(),
                "time" => new TimeCrawler(),
                _ => null,
            };
        }
    }
}