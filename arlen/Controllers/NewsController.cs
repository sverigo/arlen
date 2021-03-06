﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using arlen.Models;
using arlen.Models.ViewModel;
using arlen.Infrastructure;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using Microsoft.AspNetCore.Authorization;

namespace arlen.Controllers
{
    public class NewsController : Controller
    {
        HostingEnvironment hosting;
        NewsManager newsManager, managerForImages;
        public NewsController(ArlenContext db)
        {
            newsManager = new NewsManager(db);
            managerForImages = new NewsManager(db);
        }
        private string DRIVE_FOLDER_NAME = "user_images";

        // GET: News
        public ActionResult Index(int page=1)
        {
            var allNews = newsManager.AllNews.Reverse();
            int pageSize = 10;
                
            var count = allNews.Count();
            var items = allNews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            NewsViewModel viewModel = new NewsViewModel
            {
                PageViewModel = pageViewModel,
                PagedNewsList = items
            };

            return View(viewModel);
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            News currentNews = newsManager.FindNewsById(id);
            if (currentNews == null)
            {
                return RedirectToAction("Index");
            }
            return View(currentNews);
        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string new_image = Url.Content("/images/default-news-img.png");
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var image = Request.Form.Files[i];
                        if (image != null && image.ContentType.Contains("image/"))
                        {
                            string fileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);

                            GoogleDriveManager driveClient = new GoogleDriveManager(hosting);

                            new_image = driveClient.DriveUploadAndGetSrc(image, DRIVE_FOLDER_NAME);
                        }
                    }
                    news.Images = new_image;
                    
                    newsManager.AddNews(news);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "News");
            }
            News current = newsManager.FindNewsById(id);
            if (current == null)
            {
                return RedirectToAction("Index");
            }
            return View(current);
        }

        // POST: News/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string new_image = Url.Content("/images/default-news-img.png");
                    // EDIT EXISTING IMAGE
                    string existingImages = managerForImages.FindNewsById(news.Id).Images;
                    if (!String.IsNullOrEmpty(existingImages))
                    {
                        string[] images = existingImages.Split('|');
                        foreach (string field in Request.Form.Keys)
                        {
                            if (field.Contains("check_"))
                            {
                                var checkbox = Request.Form[field];
                                string src = field.Split('_')[1];
                                if (checkbox == "false")
                                {
                                    GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                                    driveClient.DriveMoveFileToTrash(src);
                                }
                                else
                                {
                                    new_image = src;
                                }
                            }
                        }
                    }

                    // UPLOADING NEW IMAGE
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var image = Request.Form.Files[i];
                        if (image != null && image.ContentType.Contains("image/"))
                        {
                            string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);

                            GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                            new_image = driveClient.DriveUploadAndGetSrc(image, DRIVE_FOLDER_NAME);
                        }
                    }
                    news.Images = new_image;
                    
                    newsManager.EditNews(news);
                    return Redirect("/News/Details/" + news.Id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                News n = newsManager.FindNewsById(id);
                if (n != null)
                {
                    string[] images = n.Images.Split('|');
                    foreach (string src in images)
                    {
                        if (src.Contains("default"))    //don't delete default images
                        {
                            continue;
                        }
                        GoogleDriveManager driveClient = new GoogleDriveManager(hosting);
                        driveClient.DriveMoveFileToTrash(src);
                    }
                    newsManager.RemoveNewsById(id);
                }
            }
            return RedirectToAction("Index");
        }
        
    }
}