using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class NewsManager
    {
        ArlenContext database;

        public NewsManager(ArlenContext db)
        {
            database = db;
        }

        public IEnumerable<News> AllNews
        {
            get { return database.NewsList.AsEnumerable(); }
        }

        public void AddNews(News news)
        {
            news.CreateTime = DateTime.Now;
            database.NewsList.Add(news);
            database.SaveChanges();
        }

        public void EditNews(News news)
        {
            news.CreateTime = DateTime.Now;
            database.Entry(news).State = EntityState.Modified;
            database.SaveChanges();
        }

        public void RemoveNewsById(int? id)
        {
            var removedNews = FindNewsById(id);
            if (removedNews != null)
            {
                database.NewsList.Remove(removedNews);
                database.SaveChanges();
            }
        }

        public News FindNewsById(int? id)
        {
            if (id == null || id <= 0)
                return null;
            else
            {
                News news = database.NewsList.Where(a => a.Id == id).FirstOrDefault();
                return news;
            }
        }
    }
}