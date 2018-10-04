using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Components
{
    public class EventSection : ViewComponent
    {
        IEnumerable<Event> allEvents;

        public EventSection(ArlenContext db)
        {
            EventsManager eventManager = new EventsManager(db);
            allEvents = eventManager.AllEvents.Reverse().Take(9);
        }

        public IViewComponentResult Invoke()
        {
            return View(allEvents);
        }
    }
}
