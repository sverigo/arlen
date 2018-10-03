﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class EventManager
    {
        ArlenContext database;

        public IEnumerable<Event> AllEvents
        {
            get { return database.Events.AsEnumerable(); }
        }

        public EventManager()
        {
            database = new ArlenContext();
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