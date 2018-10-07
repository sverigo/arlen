using System;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace arlen.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(String LanguageAbbreviation)
        {
            if (LanguageAbbreviation != null)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbreviation);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbreviation);
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            HttpContext.Response.Cookies.Append("Language", LanguageAbbreviation);

            return RedirectToAction("Index", "Home");
        }
    }
}