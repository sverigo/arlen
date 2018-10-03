using System.Linq;
using Microsoft.EntityFrameworkCore;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class AccountManager
    {
        ArlenContext db;

        public AccountManager()
        {
            db = new ArlenContext();
        }

        public User GetAccount(int? id)
        {
            if (id != null || id <= 0)
            {
                User user = db.Users.Where(a => a.Id == id).FirstOrDefault();
                return user;
            }
            return null;
        }

        public void EditAccount(User acc)
        {
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}