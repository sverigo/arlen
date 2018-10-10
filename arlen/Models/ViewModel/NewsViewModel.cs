using System.Collections.Generic;

namespace arlen.Models.ViewModel
{
    public class NewsViewModel
    {
        public IEnumerable<News> PagedNewsList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
