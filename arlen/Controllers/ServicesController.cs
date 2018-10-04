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
    public class ServicesController : Controller
    {
        private string DRIVE_FOLDER_FILES = "user_files";
        ServiceManager svcManager;
        public ServicesController(ArlenContext db)
        {
            svcManager = new ServiceManager(db);
        }

        // GET: Services
        public ActionResult Index()
        {
            return Redirect("/Home");
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/Home");
            }
            Service currentSvc = svcManager.FindServiceById(id);
            if (currentSvc == null)
            {
                return RedirectToAction("/Home");
            }
            return View(currentSvc);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service svc)
        {
            if (ModelState.IsValid)
            {/*
                string file_name = Request.Form["file_name"];
                string link = Request.Form["link"];
                var file = Request.Files["download"];
                if (file != null && file.ContentLength > 0)
                {
                    GoogleDriveManager driveClient = new GoogleDriveManager();
                    link = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_FILES);
                }
                svc.File = file_name + "|" + link;
                */
                svcManager.AddService(svc);
                return Redirect("/Services/Details/" + svc.Id);
            }
            else
            {
                return View();
            }
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/Home");
            }
            Service current = svcManager.FindServiceById(id);
            if (current == null)
            {
                return Redirect("/Home");
            }
            return View(current);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service svc)
        {
            if (ModelState.IsValid)
            {/*
                string file_name = Request.Form["file_name"];
                string link = Request.Form["link"];
                var file = Request.Files["download"];
                if (file != null && file.ContentLength > 0)
                {
                    GoogleDriveManager driveClient = new GoogleDriveManager();
                    link = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_FILES);
                }
                svc.File = file_name + "|" + link;
                */
                svcManager.EditService(svc);
                return Redirect("/Services/Details/" + svc.Id);
            }
            else
            {
                return View();
            }
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Service svc = svcManager.FindServiceById(id);
                if (svc != null)
                {
                    svcManager.RemoveServiceById(id);
                }
            }
            return Redirect("/Home");
        }
        
    }
}