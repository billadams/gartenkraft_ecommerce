using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;
using System.IO;

namespace Gartenkraft.Areas.Admin.Controllers.FileUploadController
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        // GET 
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase file, int? productID)
        {
            string message = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    var db = new GartenkraftEntities();

                    // check if name exist
                    if (db.tblProduct_Image.Where(pi => pi.product_image_name == file.FileName).SingleOrDefault() == null)
                    {
                        var pImages = db.tblProduct_Image.Where(pi => pi.product_id == productID).ToList();
                        // check maximum of 5 images per product
                        if (pImages.Count < 5)
                        {
                            string _FileName = Path.GetFileName(file.FileName);
                            string _dirPath = Path.Combine(Server.MapPath("~/ProductImages/"), productID + "/");

                            // create dir if not exist
                            if (!System.IO.Directory.Exists(_dirPath))
                            {
                                System.IO.Directory.CreateDirectory(_dirPath);
                            }
                            string _path = Path.Combine(_dirPath, _FileName);
                            file.SaveAs(_path);

                            // if file.saveas does not throw exception save filename in database
                            var product_image = new tblProduct_Image() { product_id = Convert.ToInt32(productID), product_image_name = file.FileName };
                            db.tblProduct_Image.Add(product_image);
                            db.SaveChanges();
                            message = "Product image succesfully uploaded.";
                        }
                        else
                        {
                            message = "Product Images have reached the maximum limit of 5 files.";
                        }
                    }
                    else
                    {
                        message = "There is an image with the same filename on file.";
                    }
                }
                return RedirectToAction("Edit", "ProductsAdmin", new { id = productID, message = message });
            }
            catch
            {
                message = "Please select a file to upload.";
                return RedirectToAction("Edit", "ProductsAdmin", new { id = productID, message = message });
            }
        }

        public ActionResult RemoveImage(int productID, string imageName)
        {
            var db = new GartenkraftEntities();
            string message = "";
            var pImage = db.tblProduct_Image.Where(pi => pi.product_id == productID && pi.product_image_name == imageName).SingleOrDefault();

            try
            {
                if (pImage != null)
                {
                    // remove file in folder
                    string _FileName = Path.GetFileName(imageName);
                    string _dirPath = Path.Combine(Server.MapPath("~/ProductImages/"), productID + "/");
                    string _path = Path.Combine(_dirPath, _FileName);
                    if (System.IO.File.Exists(_path))
                    {
                        System.IO.File.Delete(_path);

                        // remove imageFileName in database
                        db.tblProduct_Image.Remove(pImage);
                        int result = db.SaveChanges();
                        if (result == 0)
                        {
                            message = "Unable to remove the image. Please try again.";
                        }
                        else
                        {
                            message = "Product image succesfully deleted.";
                        }
                    }
                    else
                    {
                        message = "File does not exist. Please try again.";
                    }
                }
                else
                {
                    message = "Image is not on file. Please try again.";
                }
            }
            catch (Exception e)
            {
                message = "Unable to find the file. " + e.Message;
            }

            return RedirectToAction("Edit", "ProductsAdmin", new { id = productID, message = message });
        }
    }
}