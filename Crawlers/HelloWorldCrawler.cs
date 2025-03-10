namespace WebAppTest.Crawlers
{
    public class HelloWorldCrawler : ICrawler
    {
        public string Crawl()
        {
            return "Hello, World!";
        }
    }
}