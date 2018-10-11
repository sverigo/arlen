using System;
using System.Collections.Generic;
using System.Linq;
using arlen.Models;
using Microsoft.EntityFrameworkCore;

namespace arlen.Infrastructure
{
    public class EventsManager
    {
        ArlenContext database;

        public EventsManager(ArlenContext db)
        {
            database = db;
        }

        public IEnumerable<Event> AllEvents
        {
            get { return database.Events.AsEnumerable(); }
        }

        public void AddEvent(Event e)
        {
            e.CreateTime = DateTime.Now;
            database.Events.Add(e);
            database.SaveChanges();
        }

        public void EditEvent(Event e)
        {
            e.CreateTime = DateTime.Now;
            Event ex = database.Entry(e).Entity;
            
            database.Entry(e).Property("CreateTime").IsModified = true;
            database.Entry(e).State = EntityState.Modified;

            database.SaveChanges();
        }

        public void RemoveEventById(int? id)
        {
            var e = FindEventById(id);
            if (e != null)
            {
                database.Events.Remove(e);
                database.SaveChanges();
            }
        }

        public Event FindEventById(int? id)
        {
            if (id == null || id <= 0)
                return null;
            else
            {
                Event e = database.Events.Where(a => a.Id == id).FirstOrDefault();
                return e;
            }
        }
    }
}