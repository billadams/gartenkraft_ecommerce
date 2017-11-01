using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gartenkraft_Admin.Controllers.FileUploadController
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
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath(Url.Content("~/ProductImages")), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File uploaded successfully";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed.";
                return View();
            }
        }
    }
}