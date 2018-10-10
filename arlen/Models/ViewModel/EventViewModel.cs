using System.Collections.Generic;

namespace arlen.Models.ViewModel
{
    public class EventViewModel
    {
        public IEnumerable<Event> PagedEventList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
