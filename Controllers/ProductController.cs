using ProjectScrum.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectScrum.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        db_testEntities1 dbObj = new db_testEntities1();  /*Databse obj*/
        public ActionResult ViewPrdoct()                /*List of products*/
        {
            try {

                var res = dbObj.ProductDetails.ToList();
                return View(res);
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public ActionResult CreateProduct() {           /* Add Product View*/
            return View();
        }

        [HttpPost]
        public ActionResult CreatePrdoct(ProductDetail Details, HttpPostedFileBase image)   /*Adding Product to DBA*/
        {
            try
            {
                ProductDetail obj = new ProductDetail();
                obj.Id = Details.Id;

                obj.Product_title = Details.Product_title;

                obj.Price = Details.Price;
                obj.Stock = Details.Stock;
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    image.SaveAs(imagePath);

                    obj.ImageURL = "/Uploads/" + fileName;
                }

                dbObj.ProductDetails.Add(obj);
                dbObj.SaveChanges();

                return Json(new { success = true, message = "Product added successfully!" });
            }
            catch (Exception ex) {
                return Json(new { success = false, message = "Error adding product: " + ex.Message });
            }
        }


        public ActionResult DeletePrdoct(int id)                        /*Delete Product to DBA*/
        {
            var res = dbObj.ProductDetails.Where(x => x.Id == id).First();
            dbObj.ProductDetails.Remove(res);
            dbObj.SaveChanges();

            var Lis = dbObj.ProductDetails.ToList();

            return View("ViewPrdoct", Lis);
        }

        public ActionResult UpdateView(int id=0)                        /*Edit Product View */
        {
            if (id != 0)
            {
                ProductDetail res = new ProductDetail();
                res = dbObj.ProductDetails.Where(x => x.Id == id).First();
                ViewBag.Product_title = res.Product_title;
                ViewBag.ImageURL = res.ImageURL;
                ViewBag.Price = res.Price;
                ViewBag.Stock = res.Stock;
                ViewBag.id = res.Id;
            }
            return View();

        }

        [HttpPost]
        public ActionResult UpdatePrdoct(ProductDetail Details, HttpPostedFileBase image)   /*Edit Product to DBA*/
        {
            try
            {
                ProductDetail obj = new ProductDetail();

                ProductDetail existingProduct = dbObj.ProductDetails.Find(Details.Id);

                existingProduct.Product_title = Details.Product_title;
                existingProduct.Price = Details.Price;
                existingProduct.Stock = Details.Stock;
                if (image != null && image.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    image.SaveAs(imagePath);

                    obj.ImageURL = "/Uploads/" + fileName;
                }
                else
                {
                    var res = dbObj.ProductDetails.Where(x => x.Id == Details.Id).First();
                    existingProduct.ImageURL = res.ImageURL;
                }

                dbObj.SaveChanges();

                return Json(new { success = true, message = "Product added successfully!" });
            }
            catch (Exception ex) {
                return Json(new { success = true, message = ex });
            }

        }



        public ActionResult Chart() {               /*Graph View*/
            return View();
        }
        [HttpGet]
        public ActionResult GetChartData()    /*Graph Details*/
        {
            var res = dbObj.ProductDetails.ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
    }
}