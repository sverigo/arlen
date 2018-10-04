using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Components
{
    public class PartnerSection : ViewComponent
    {
        IEnumerable<Partner> allPartners;

        public PartnerSection(ArlenContext db)
        {
            PartnerManager partnerManager = new PartnerManager(db);
            allPartners = partnerManager.All;
        }

        public IViewComponentResult Invoke()
        {
            return View(allPartners);
        }
    }
}
