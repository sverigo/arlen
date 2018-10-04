using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;

namespace arlen.Controllers
{
    public class PromosController : Controller
    {
        PromosManager pmManager;
        public PromosController(ArlenContext db)
        {
            pmManager = new PromosManager(db);
        }
        private string DRIVE_FOLDER_NAME = "user_images";

        // GET: Promos
        public ActionResult Index()
        {
            IEnumerable<Promo> allPromos = pmManager.All;
            return View(allPromos);
        }

        // GET: Promos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Promo pm)
        {
            if (ModelState.IsValid)
            {/*
                var file = Request.Files["downl"];
                if (file != null && file.ContentType.Contains("image/"))
                {
                    string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    GoogleDriveManager driveClient = new GoogleDriveManager();

                    pm.Image = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_NAME);
                }*/
                pmManager.Add(pm);
                return Redirect("/Promos");
            }
            else
            {
                return View();
            }
        }

        // GET: Promos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Promo pm = pmManager.FindById(id);
            if (pm == null)
            {
                return RedirectToAction("Index");
            }
            return View(pm);
        }

        // POST: Promos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Promo pm)
        {
            if (ModelState.IsValid)
            {/*
                var file = Request.Files["downl"];
                if (file != null && file.ContentType.Contains("image/"))
                {
                    string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    GoogleDriveManager driveClient = new GoogleDriveManager();

                    pm.Image = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_NAME);
                }*/
                pmManager.Edit(pm);
                return Redirect("/Promos");
            }
            else
            {
                return View();
            }
        }

        // GET: Promos/Delete/5
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