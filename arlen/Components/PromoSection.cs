using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Components
{
    public class PromoSection : ViewComponent
    {
        IEnumerable<Promo> allPromos;

        public PromoSection(ArlenContext db)
        {
            PromosManager promosManager = new PromosManager(db);
            allPromos = promosManager.All.Take(5);
        }

        public IViewComponentResult Invoke()
        {
            return View(allPromos);
        }
    }
}