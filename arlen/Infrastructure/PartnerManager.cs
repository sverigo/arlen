using System.Collections.Generic;
using System.Linq;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class PartnerManager
    {
        ArlenContext database;
        public PartnerManager(ArlenContext db)
        {
            database = db;
        }

        public IEnumerable<Partner> All
        {
            get { return database.Partners.AsEnumerable(); }
        }

        public void Add(Partner pm)
        {
            database.Partners.Add(pm);
            database.SaveChanges();
        }

        public void Edit(Partner pm)
        {
            database.Entry(pm).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            database.SaveChanges();
        }

        public void RemoveById(int? id)
        {
            var pm = FindById(id);
            if (pm != null)
            {
                database.Partners.Remove(pm);
                database.SaveChanges();
            }
        }

        public Partner FindById(int? id)
        {
            if (id == null || id <= 0)
                return null;
            else
            {
                Partner pm = database.Partners.Where(a => a.Id == id).FirstOrDefault();
                return pm;
            }
        }
    }
}
