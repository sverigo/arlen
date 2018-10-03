using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class PromoManager
    {
        ArlenContext database;

        public IEnumerable<Promo> All
        {
            get { return database.Promos.AsEnumerable(); }
        }

        public PromoManager()
        {
            database = new ArlenContext();
        }

        public void Add(Promo pm)
        {
            database.Promos.Add(pm);
            database.SaveChanges();
        }

        public void Edit(Promo pm)
        {
            database.Entry(pm).State = EntityState.Modified;
            database.SaveChanges();
        }

        public void RemoveById(int? id)
        {
            var pm = FindById(id);
            if (pm != null)
            {
                database.Promos.Remove(pm);
                database.SaveChanges();
            }
        }

        public Promo FindById(int? id)
        {
            if (id == null || id <= 0)
                return null;
            else
            {
                Promo pm = database.Promos.Where(a => a.Id == id).FirstOrDefault();
                return pm;
            }
        }
    }
}