using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class ServiceManager
    {
        ArlenContext database;
        public ServiceManager(ArlenContext db)
        {
            database = db;
        }

        public IEnumerable<Service> AllServices
        {
            get { return database.Services.AsEnumerable(); }
        }

        public void AddService(Service svc)
        {
            database.Services.Add(svc);
            database.SaveChanges();
        }

        public void EditService(Service svc)
        {
            database.Entry(svc).State = EntityState.Modified;
            database.SaveChanges();
        }

        public void RemoveServiceById(int? id)
        {
            var svc = FindServiceById(id);
            if (svc != null)
            {
                database.Services.Remove(svc);
                database.SaveChanges();
            }
        }

        public Service FindServiceById(int? id)
        {
            if (id == null || id <= 0)
                return null;
            else
            {
                Service svc = database.Services.Where(a => a.Id == id).FirstOrDefault();
                return svc;
            }
        }
    }
}
