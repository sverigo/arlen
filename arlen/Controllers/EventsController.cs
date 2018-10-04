using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Infrastructure;
//using PagedList;

namespace arlen.Controllers
{
    public class EventsController : Controller
    {
        EventsManager eventsManager, managerForImages;
        public EventsController(ArlenContext db)
        {
            eventsManager = new EventsManager(db);
            managerForImages = new EventsManager(db);
        }
        private string DRIVE_FOLDER_IMAGES = "user_images";
        private string DRIVE_FOLDER_FILES = "user_files";

        // GET: Events
        public ActionResult Index(int? page)
        {
            var events = eventsManager.AllEvents;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(events.OrderByDescending(a => a.Id)/*.ToPagedList(pageNumber, pageSize)*/);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Event e = eventsManager.FindEventById(id);
            if (e == null)
            {
                return RedirectToAction("Index");
            }
            return View(e);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Event e)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*string new_image = Url.Content("~/Content/images/default-event-img.png");
                    var image = Request.Files["Images"];
                    if (image != null && image.ContentType.Contains("image/"))
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        GoogleDriveManager driveClient = new GoogleDriveManager();
                        new_image = driveClient.DriveUploadAndGetSrc(image, DRIVE_FOLDER_IMAGES);
                    }
                    e.Images = new_image;

                    string file_name = Request.Form["file_name"];
                    string link = Request.Form["link"];
                    var file = Request.Files["download"];
                    if (file != null && file.ContentLength > 0)
                    {
                        GoogleDriveManager driveClient = new GoogleDriveManager();
                        link = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_FILES);
                    }
                    e.Files = file_name + "|" + link;
                    */
                    eventsManager.AddEvent(e);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();

        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Events");
            }
            Event current = eventsManager.FindEventById(id);
            if (current == null)
            {
                return RedirectToAction("Index");
            }
            return View(current);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Event e)
        {
            if (ModelState.IsValid)
            {
                try
                {/*
                    string new_image = Url.Content("~/Content/images/default-event-img.png");
                    // EDIT EXISTING IMAGES
                    string existingImages = managerForImages.FindEventById(e.Id).Images;
                    if (!String.IsNullOrEmpty(existingImages))
                    {
                        string[] images = existingImages.Split('|');
                        foreach (string field in Request.Form)
                        {
                            if (field.Contains("check_"))
                            {
                                var checkbox = Request.Form[field];
                                string src = field.Split('_')[1];
                                if (checkbox == "false" && !src.Contains("default"))
                                {
                                    GoogleDriveManager driveClient = new GoogleDriveManager();
                                    driveClient.DriveMoveFileToTrash(src);
                                }
                                else
                                {
                                    new_image = src;
                                }
                            }
                        }
                    }

                    // UPLOADING NEW IMAGES
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var image = Request.Files[i];
                        if (image != null && image.ContentType.Contains("image/"))
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                            GoogleDriveManager driveClient = new GoogleDriveManager();
                            new_image = driveClient.DriveUploadAndGetSrc(image, DRIVE_FOLDER_IMAGES);
                        }
                    }
                    e.Images = new_image;

                    // Зарузка и формирование файла
                    string file_name = Request.Form["file_name"];
                    string link = Request.Form["link"];
                    var file = Request.Files["download"];
                    if (file != null && file.ContentLength > 0)
                    {
                        GoogleDriveManager driveClient = new GoogleDriveManager();
                        link = driveClient.DriveUploadAndGetSrc(file, DRIVE_FOLDER_FILES);
                    }
                    e.Files = file_name + "|" + link;
                    */

                    //delete next line when CORE completed:
                    e.Files = "1|2";

                    eventsManager.EditEvent(e);
                    return Redirect("/Events/Details/" + e.Id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Event e = eventsManager.FindEventById(id);
                if (e != null)
                {
                    /*string[] images = e.Images.Split('|');
                    foreach (string src in images)
                    {
                        if (src.Contains("default"))    //don't delete default image
                        {
                            continue;
                        }
                        GoogleDriveManager driveClient = new GoogleDriveManager();
                        driveClient.DriveMoveFileToTrash(src);
                    }*/
                    eventsManager.RemoveEventById(id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}