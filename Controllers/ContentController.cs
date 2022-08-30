using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.ViewModel;

namespace Ecommerce.Controllers
{
    [RoutePrefix("Content")]
    [ValidateInput(false)]
    public class ContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Retrive content from database 
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["cart"] != null)
            {
                double x = 0;
                List<Content> li2 = TempData["Cart"] as List<Content>;
                foreach (var item in li2)
                {
                    x += item.bill;

                }

                TempData["total"] = x;
            }
           
           
            TempData.Keep();
         db.Contents.OrderByDescending(x => x.ID).ToList();
        var content = db.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Contents,
                s.Description,
                s.Colour,
                s.Price,
                s.Quantity,
                s.size

            });

            List<ContentViewModel> contentModel = content.Select(item => new ContentViewModel()
            {
                ID = item.ID,
                Title = item.Title,
                Image = item.Image,
                Description = item.Description,
               Colour = item.Colour,
               Price = item.Price,
               Quantity = item.Quantity,
               size = item.size,
                Contents = item.Contents

                
            }).ToList();

            return View(contentModel);
           
           
        }
        public ActionResult AddToCart(int? Id)
        {
            var content = db.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Contents,
                s.Description,
                s.Colour,
                s.Price,
                s.Quantity,
                s.size

            });
          

            Content p = db.Contents.Where(x => x.ID == Id).SingleOrDefault();
            return View(p);
        }
       
        List<Content> li = new List<Content>();
        [HttpPost]
        public ActionResult AddToCart(Content pi, string qty, int Id)
        {
            
            Content p = db.Contents.Where(x => x.ID == Id).SingleOrDefault();
           
            Content c = new Content();
            c.ID = p.ID;
            c.Price = p.Price;
            c.Colour = p.Colour;
            c.size = p.size;
            c.Quantity = Convert.ToInt32(qty);
            c.bill = c.Price * c.Quantity;
            c.total = c.total + c.bill;
           



            c.Title = p.Title;


            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<Content> li2 = TempData["cart"] as List<Content>;
                li2.Add(c);
                TempData["cart"] = li2;
            }

            TempData.Keep();




            return RedirectToAction("Index");
        }
        
        public ActionResult Checkout()
        {
            var content = db.Contents.Select(s => new
            {
                s.ID,
                s.Title,
                s.Image,
                s.Contents,
                s.Description,
                s.Colour,
                s.Price,
                s.Quantity,
                s.size,
                s.bill

            });
            
           

        TempData.Keep();
            return View();



        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Contents where temp.ID == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ContentViewModel model)
        {
            
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDataBase(file, model);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete()
        {
            return View(db.Contents.ToList());
        }
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: CarQuotes/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int id)
        {
            Content content = db.Contents.Find(id);
            db.Contents.Remove(content);
            db.SaveChanges();
            return RedirectToAction("Delete");
        }
        public ActionResult Removeitem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Contents.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: CarQuotes/Delete/5
        [HttpPost, ActionName("Removeitem")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveitemConfirmed(int id)
        {
            Content content = db.Contents.Find(id);
            db.Contents.Remove(content);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}