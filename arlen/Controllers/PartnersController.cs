using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using arlen.Infrastructure;
using arlen.Models;
using Microsoft.AspNetCore.Hosting.Internal;

namespace arlen.Controllers
{
    public class PartnersController : Controller
    {
        PartnerManager pmManager;
        HostingEnvironment hosting;
        public PartnersController(ArlenContext db)
        {
            pmManager = new PartnerManager(db);
        }
        private string DRIVE_FOLDER_NAME = "user_images";

        // GET: Partners
        public ActionResult Index()
        {
            IEnumerable<Partner> allPromos = pmManager.All;
            return View(allPromos);
        }

        // GET: Partners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Partner pm)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files["downl"];
                if (file != null && file.ContentType.Contains("image/"))
                {
                    string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    GoogleDriveManager driveClient = new GoogleDriveManager(hosting);

                    pm.Image = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_NAME);
                }
                pmManager.Add(pm);
                return Redirect("/Partners");
            }
            else
            {
                return View();
            }
        }

        // GET: Partners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Partner pm = pmManager.FindById(id);
            if (pm == null)
            {
                return RedirectToAction("Index");
            }
            return View(pm);
        }

        // POST: Partners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Partner pm)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files["downl"];
                if (file != null && file.ContentType.Contains("image/"))
                {
                    string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    GoogleDriveManager driveClient = new GoogleDriveManager(hosting);

                    pm.Image = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_NAME);
                }
                pmManager.Edit(pm);
                return Redirect("/Partners");
            }
            else
            {
                return View();
            }
        }

        // GET: Partners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                pmManager.RemoveById(id);
            }
            return RedirectToAction("Index");
        }
        
    }
}