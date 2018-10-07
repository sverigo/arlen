using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Controllers
{
    public class HomeController : Controller
    {
        AccountManager accManager;
        ServiceManager svcManager;

        public HomeController(ArlenContext context)
        {
            accManager = new AccountManager(context);
            svcManager = new ServiceManager(context);
        }

        // Home Page
        public IActionResult Index()
        {
            User account = accManager.GetAccount(1);
            IEnumerable<Service> services = svcManager.AllServices;

            string[] files = account.Files.Split('|');
            List<string> items = new List<string>();
            Dictionary<string, object> model = new Dictionary<string, object>();
            foreach (string line in files)
            {
                string[] temp = line.Split('^');
                if (temp.Length < 2)
                {
                    items.Add("");
                    items.Add("");
                }
                else
                {
                    items.Add(temp[0]);
                    items.Add(temp[1]);
                }
            }

            model.Add("aboutImage", account.AboutImage);
            model.Add("aboutVideo", account.AboutVideo);
            model.Add("aboutText", account.AboutText);
            model.Add("aboutVideoMode", account.AboutVideoMode);
            model.Add("files", items);
            model.Add("services", services);

            return View(model);
        }
    }
}