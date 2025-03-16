using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebAppTest.Crawlers;

namespace WebAppTest.Factories
{
    public interface ICrawlerFactory
    {
        ICrawler CreateCrawler(string type);
    }

    public class CrawlerFactory : ICrawlerFactory
    {
        private readonly Dictionary<string, Type> _crawlers;

        public CrawlerFactory()
        {
            _crawlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICrawler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToDictionary(t => t.Name.ToLower().Replace("crawler", ""), t => t);
        }

        public ICrawler CreateCrawler(string type)
        {
            if (_crawlers.TryGetValue(type.ToLower(), out var crawlerType))
            {
                return (ICrawler)Activator.CreateInstance(crawlerType);
            }

            throw new ArgumentException($"Crawler type '{type}' is not supported.", nameof(type));
        }
    }
}