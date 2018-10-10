using arlen.Models;
using arlen.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace arlen.Components
{
    public class MapSection : ViewComponent
    {
        User user = null;
        public MapSection(ArlenContext context)
        {
            AccountManager manager = new AccountManager(context);
            user = manager.GetAccount(1);
        }

        public IViewComponentResult Invoke()
        {
            return View(user);
        }
    }
}
