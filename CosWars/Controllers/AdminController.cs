using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosWars.Models;
using System.Data.Entity;
using System.Web.Helpers;
using System.IO;

namespace CosWars.Controllers
{
    public class AdminController : Controller
    {
        DB_CosWarsEntities db = new DB_CosWarsEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region CATEGORY
        public ActionResult CategoryList()
        {
            return View(db.Kategori.ToList());
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Kategori AddCategory)
        {
            if (ModelState.IsValid)
            {
                AddCategory.EklenmeTarihi = DateTime.Now;
                if (AddCategory.Sira == null)
                {
                    AddCategory.Sira = 0;
                }
                db.Kategori.Add(AddCategory);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                return View();
            }

            return RedirectToAction("CategoryList", "Admin");
        }

        public ActionResult EditCategory(int? id)
        {
            try
            {
                Kategori kategori = db.Kategori.Find(id);

                if (kategori != null)
                    return View(kategori);

                return RedirectToAction("CategoryList", "Admin");
            }
            catch (Exception)
            {
                return RedirectToAction("CategoryList", "Admin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Kategori EditCategory)
        {

            if (ModelState.IsValid)
            {
                db.Entry(EditCategory).State = EntityState.Modified;

                if (EditCategory.Sira == null)
                    EditCategory.Sira = 0;

                db.SaveChanges();
                return RedirectToAction("CategoryList", "Admin");
            }
            else
            {

                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><h4><i class=\"icon fa fa-warning\"></i> Error!</h4>Please check the entered information.</div>";
                return View(EditCategory);
            }
        }

        public ActionResult DeleteCategory(int? id)
        {
            Kategori DeleteCategory = db.Kategori.Find(id);

            if (DeleteCategory == null)
                return RedirectToAction("CategoryList", "Admin");

            db.Kategori.Remove(DeleteCategory);
            db.SaveChanges();

            return RedirectToAction("CategoryList", "Admin");
        }

        #endregion

        #region TAG
        public ActionResult TagList()
        {
            return View(db.Etiket.ToList());
        }

        public ActionResult AddTag()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTag(Etiket AddTag)
        {
            if (ModelState.IsValid)
            {
                AddTag.EklenmeTarihi = DateTime.Now;
                AddTag.Adi = AddTag.Adi.ToLower();
                if (AddTag.Sira == null)
                {
                    AddTag.Sira = 0;
                }
                db.Etiket.Add(AddTag);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                return View();
            }

            return RedirectToAction("TagList", "Admin");
        }

        public ActionResult EditTag(int? id)
        {
            try
            {
                Etiket Etiket = db.Etiket.Find(id);

                if (Etiket != null)
                    return View(Etiket);

                return RedirectToAction("TagList", "Admin");
            }
            catch (Exception)
            {
                return RedirectToAction("TagList", "Admin");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTag(Etiket EditTag)
        {

            if (ModelState.IsValid)
            {
                EditTag.Adi = EditTag.Adi.ToLower();
                db.Entry(EditTag).State = EntityState.Modified;

                if (EditTag.Sira == null)
                    EditTag.Sira = 0;

                db.SaveChanges();
                return RedirectToAction("TagList", "Admin");
            }
            else
            {

                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><h4><i class=\"icon fa fa-warning\"></i> Error!</h4>Please check the entered information.</div>";
                return View(EditTag);
            }
        }

        public ActionResult DeleteTag(int? id)
        {
            Etiket DeleteTag = db.Etiket.Find(id);

            if (DeleteTag == null)
                return RedirectToAction("TagList", "Admin");

            db.Etiket.Remove(DeleteTag);
            db.SaveChanges();

            return RedirectToAction("TagList", "Admin");
        }

        #endregion

        #region COSPLAY

        public ActionResult CosplayList()
        {
            return View(db.Cosplay.ToList());
        }

        public ActionResult AddCosplay()
        {
            ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi");
            ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCosplay(Cosplay AddCosplay, HttpPostedFileBase file, int[] Tags)
        {
            if (ModelState.IsValid && file != null)
            {
                #region  Görsel
                if (file.ContentLength > 0 && file.ContentLength <= 3000000)
                {
                    if (file.ContentType == "image/png" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg")
                    {
                        var path = Server.MapPath("~/Content/Cosplay/");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        string dosyayolu = String.Empty;
                        string resimAdi = "/Content/Cosplay/" + "cos-" + Guid.NewGuid().ToString().Substring(0, 8) + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        dosyayolu = Server.MapPath(resimAdi);
                        file.SaveAs(dosyayolu);

                        WebImage resim = new WebImage(dosyayolu);
                        resim.Resize(782, 782, preserveAspectRatio: false, preventEnlarge: false).Crop(1, 1, 1, 1);
                        resim.Save();

                        if (AddCosplay.Sira == null)
                                AddCosplay.Sira = 0;

                        if (Tags != null)
                        {
                            foreach (var item in Tags)
                            {
                                var tag = db.Etiket.Find(item);
                                AddCosplay.Etiket.Add(tag);
                            }
                        }

                        AddCosplay.ResimYolu = resimAdi;
                        AddCosplay.EklenmeTarihi = DateTime.Now;
                        db.Cosplay.Add(AddCosplay);
                        db.SaveChanges();

                        return RedirectToAction("CosplayList", "Admin");
                    }
                    else
                    {
                        ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi");
                        ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                        ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Image format not supported.</div>";
                        return View();
                    }
                }
                else
                {
                    ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi");
                    ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                    ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong>  Maximum allowed image size 3MB.</div>";
                    return View();
                }
                #endregion
            }
            else
            {
                ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi");
                ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                return View();
            }
        }

        public ActionResult EditCosplay(int? id)
        {
            try
            {
                Cosplay EditCosplay = db.Cosplay.Find(id);

                if (EditCosplay != null)
                {
                    List<int> selected = EditCosplay.Etiket.Select(a => a.ID).ToList(); //sadece ID numaraları listeye atanacağı için select metodu kullanıldı
                    ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi", selected);
                    ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                    return View(EditCosplay);
                }

                return RedirectToAction("CosplayList", "Admin");
            }
            catch (Exception)
            {
                return RedirectToAction("CosplayList", "Admin");
            }            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCosplay(Cosplay EditCosplay, HttpPostedFileBase file, int[] Tags, string ResimYolu)
        {
            if (ModelState.IsValid)
            {
                #region ResimDuzenle
                if (file != null)
                {
                    if (file.ContentLength > 0 && file.ContentLength < 3000000)
                    {
                        if (file.ContentType == "image/png" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg")
                        {
                            var path = Server.MapPath("~/Content/Cosplay/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            string dosyayolu = String.Empty;
                            string resimAdi = "/Content/Cosplay/" + "cos-" + Guid.NewGuid().ToString().Substring(0, 8) + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                            dosyayolu = Server.MapPath(resimAdi);
                            file.SaveAs(dosyayolu);

                            try
                            {
                                string SilResim = Server.MapPath(ResimYolu);
                                System.IO.File.Delete(SilResim);
                            }
                            catch (Exception)
                            {
                            }

                            EditCosplay.ResimYolu = resimAdi; // Yeni Resim Yolu

                            WebImage resim = new WebImage(dosyayolu);
                            resim.Resize(782, 782, preserveAspectRatio: false, preventEnlarge: false).Crop(1, 1, 1, 1);
                            resim.Save();
                        }
                        else
                        {
                            List<int> selected = EditCosplay.Etiket.Select(a => a.ID).ToList(); //sadece ID numaraları listeye atanacağı için select metodu kullanıldı
                            ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi", selected);
                            ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                            ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                            return View(EditCosplay);
                        }
                    }
                    else
                    {
                        List<int> selected = EditCosplay.Etiket.Select(a => a.ID).ToList(); //sadece ID numaraları listeye atanacağı için select metodu kullanıldı
                        ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi", selected);
                        ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                        ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                        return View(EditCosplay);
                    }
                }
                #endregion

                if (EditCosplay.Sira == null)
                    EditCosplay.Sira = 0;                

                Cosplay CurrentCosplay = db.Cosplay.Find(EditCosplay.ID);

                EditCosplay.Etiket = CurrentCosplay.Etiket;
                EditCosplay.Etiket.Clear();

                if (Tags != null)
                {
                    foreach (var item in Tags)
                    {
                        EditCosplay.Etiket.Add(db.Etiket.Find(item));
                    }
                }
                db.Entry(CurrentCosplay).CurrentValues.SetValues(EditCosplay);
                //db.Entry(EditCosplay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CosplayList", "Admin");

            }
            else
            {
                List<int> selected = EditCosplay.Etiket.Select(a => a.ID).ToList(); //sadece ID numaraları listeye atanacağı için select metodu kullanıldı
                ViewData["CosplayTag"] = new MultiSelectList(db.Etiket, "ID", "Adi", selected);
                ViewData["CosplayCategory"] = new SelectList(db.Kategori, "ID", "Adi");

                ViewBag.Message = "<div class=\"alert alert-warning alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\" >×</button><strong><i class=\"icon fa fa-warning\"></i> Error!</strong> Please check the entered information.</div>";
                return View(EditCosplay);
            }

        }

        public ActionResult DeleteCosplay(int? id)
        {
            Cosplay DeleteCosplay = db.Cosplay.Find(id);

            if (DeleteCosplay == null)
                return RedirectToAction("CosplayList", "Admin");

            if (DeleteCosplay.ResimYolu != null)            
                    System.IO.File.Delete(Server.MapPath(DeleteCosplay.ResimYolu));            

            db.Cosplay.Remove(DeleteCosplay);
            db.SaveChanges();

            return RedirectToAction("CosplayList", "Admin");
        }
        #endregion
    }
}