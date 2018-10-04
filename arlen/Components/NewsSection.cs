using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Components
{
    public class NewsSection : ViewComponent
    {
        IEnumerable<News> allNews;

        public NewsSection(ArlenContext db)
        {
            NewsManager newsManager = new NewsManager(db);
            allNews = newsManager.AllNews.Reverse().Take(9);
        }

        public IViewComponentResult Invoke()
        {
            return View(allNews);
        }
    }
}
