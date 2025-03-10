using System;

namespace WebAppTest.Crawlers
{
    public class TimeCrawler : ICrawler
    {
        public string Crawl()
        {
            return DateTime.Now.ToString("o");
        }
    }
}