using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace arlen.Controllers
{
    public class AccountController : Controller
    {
        static string DRIVE_FOLDER_NAME = "user_files";
        ArlenContext database;
        HostingEnvironment hosting;
        public AccountController(ArlenContext db)
        {
            database = db;
        }
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (database)
                {
                    user = database.Users.FirstOrDefault(u => u.Login == model.Name && u.Password == model.Password);

                }
                if (user != null)
                {
                    await Authenticate(model.Name);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }
            return View(model);
        }

        // GET: Account/Settings
        [Authorize]
        public ActionResult Settings()
        {
            AccountManager manager = new AccountManager(database);
            //1 is for main Admin user
            User account = manager.GetAccount(1);
            return View(account);
        }

        // POST: Account/Settings
        [HttpPost]
        [Authorize]
        public ActionResult Settings(User account)
        {
            AccountManager manager = new AccountManager(database);
            string exSep = "|";
            string inSep = "^";
            string[] files = new string[3];

            // Загрузка логотипа
            
            var logoImage = Request.Form.Files["downl-logo"];
            if (logoImage != null && logoImage.Length > 0 && logoImage.ContentType.Contains("image"))
            {
                GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                account.Logo = driveClient.DriveUploadAndGetSrc(logoImage, DRIVE_FOLDER_NAME);
            }

            // Загрузка изображения в раздел "О нас"
            var aboutImage = Request.Form.Files["downl-about"];
            if (aboutImage != null && aboutImage.Length > 0 && aboutImage.ContentType.Contains("image"))
            {
                GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                account.AboutImage = driveClient.DriveUploadAndGetSrc(aboutImage, DRIVE_FOLDER_NAME);
            }
            
            // Телефоны
            List<string> phonesList = new List<string>();
            foreach (string field in Request.Form.Keys)
            {
                if (field.Contains("Phone"))
                {
                    phonesList.Add(Request.Form[field]);
                }
            }
            account.Phones = string.Join("|", phonesList);

            // Загрузка файлов на главной странице
            for (int i = 0; i < Request.Form.Files.Count - 2; i++)
            {
                string title = Request.Form["title" + i.ToString()];
                string link = Request.Form["link" + i.ToString()];
                var file = Request.Form.Files["downl" + i];
                if (file != null && file.Length > 0)
                {
                    
                    GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                    link = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_NAME);
                }
                if (title != null && title.Length > 0 || link != null && link.Length > 0)
                    files[i] = title + inSep + link;
                else
                    files[i] = "";
            }
            account.Files = string.Join(exSep, files);
            
            account.Id = 1; /*стоит здесь, пока юзер в бд один*/
            if (account.Password == null || account.Password == string.Empty)
            {
                account.Password = Request.Form["oldPass"];
            }

            if (ModelState.IsValid)
            {
                manager.EditAccount(account);
            }
            return Redirect("/Account/Settings");
        }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}