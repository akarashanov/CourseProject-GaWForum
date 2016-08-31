using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var latestPosts = db.Posts.Include(p => p.Author).OrderByDescending(p=>p.Date).Take(3);

            return View(latestPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "What is Galaxy at War:Clone Wars";

            return View();
        }

        public ActionResult Articles()
        {
            ViewBag.Message = "All articles about GaW";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Where to find us:";

            return View();
        }
    }
}