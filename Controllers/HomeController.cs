using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodBankMVC.Models;

namespace BloodBankMVC.Controllers
{
    public class HomeController : Controller
    {
        BBEntities db = new BBEntities();
        public ActionResult Index()
        {
            var grp = db.BloodGroups.ToList();
            var dst = db.Districts.ToList();
            var abt = db.Posts.Where(p => p.CategoryID == 2 && p.Status == true).ToList().Take(3);
            var vul = db.Posts.Select(p => new { p.URL, p.CategoryID, p.Status }).Where(p => p.CategoryID == 4 && p.Status == true).FirstOrDefault();

            ViewBag.groups = grp;
            ViewBag.districts = dst;
            ViewBag.about_us = abt;
            ViewBag.vieoUrl = vul;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}