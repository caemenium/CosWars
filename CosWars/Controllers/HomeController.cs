using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosWars.Models;

namespace CosWars.Controllers
{
    public class HomeController : Controller
    {
        DB_CosWarsEntities db = new DB_CosWarsEntities();

        //TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        //title = textInfo.ToTitleCase(title); //War And Peace

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Battle()
         {
            db.Configuration.LazyLoadingEnabled = false;
            var battle = db.Cosplay.Where(c => c.AktifMi == true).OrderBy(c => Guid.NewGuid()).Take(2).ToArray();
            return View(battle);

            //if (IsVote)
            //{
            //    var battle = db.Cosplay.Where(c => c.AktifMi == true).OrderBy(c => Guid.NewGuid()).Take(2);
            //    Session["IsVote"] = battle;
            //    return View(battle.ToArray());
            //}
            //else if (IsVote == false && Session["IsVote"] == null)
            //{
            //    var battle = db.Cosplay.Where(c => c.AktifMi == true).OrderBy(c => Guid.NewGuid()).Take(2);
            //    Session["IsVote"] = battle;
            //    return View(battle.ToArray());
            //}
            //else
            //{
            //    var battle = Session["IsVote"] as List<Cosplay>;
            //    //Session["IsVote"] = null;
            //    return View(battle.ToArray());
            //}
        }

            public ActionResult Vote(int? id)
        {
            try
            {
                var cosplay = db.Cosplay.Find(id);
                if (cosplay != null)
                {
                    cosplay.Vote++;
                    db.SaveChanges();
                    return RedirectToAction("Battle", "Home");
                }

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //[HttpPost]
        //public ActionResult Index()
        //{
        //    var battle = db.Cosplay.Where(c => c.AktifMi == true).OrderBy(c => Guid.NewGuid()).Take(2).ToArray();
        //    return View(battle);
        //}
        public ActionResult About1()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About2()
        {
            ViewBag.Message = "Your application description page.";

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