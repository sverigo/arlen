using System.Linq;
using arlen.Models;

namespace arlen.Infrastructure
{
    public class AccountManager
    {
        ArlenContext database;
        public AccountManager(ArlenContext db)
        {
            database = db;
        }

        public User GetAccount(int? id)
        {
            if (id != null || id <= 0)
            {
                User user = database.Users.Where(a => a.Id == id).FirstOrDefault();
                return user;
            }
            return null;
        }

        public void EditAccount(User acc)
        {
            database.Entry(acc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            database.SaveChanges();
        }
    }
}
