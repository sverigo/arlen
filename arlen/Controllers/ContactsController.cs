using Microsoft.AspNetCore.Mvc;
using arlen.Infrastructure;
using arlen.Models;

namespace arlen.Controllers
{
    public class ContactsController : Controller
    {
        AccountManager accManager;
        public ContactsController(ArlenContext db)
        {
            accManager = new AccountManager(db);
        }
        
        // Feedback
        [HttpPost]
        public ActionResult Feedback()
        {
            if (Request.Form["g-recaptcha-response"] != string.Empty)
            {
                User acc = accManager.GetAccount(1);    

                string msg = Request.Form["message"];
                string email = Request.Form["email"];
                string phone = Request.Form["phone"];
                string name = Request.Form["name"];

                string subject = "New Feedback - Arlen";

                string text = "Hi <br>" +
                    "You got a new reply from user: <strong>" + name + "</strong><br>" +
                    "<strong>Email:</strong> " + email + ". <strong>Phone:</strong> " + phone + "<br>" +
                    "<br><div style='border-left: 2px solid blue;'><div style='margin: 5px;'>" + msg + "</div></div>";

                EmailService svc = new EmailService();
                svc.SendEmail(acc.AccountEmail, subject, text);
                return View("Success");
            }
            return View("Error");
        }
    }
}