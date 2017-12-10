using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Users = db.AspNetUsers.ToList();
            ViewBag.Orders = db.tblSales_Invoice.ToList();

            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View();
            }
            

            return RedirectToAction("Login", "Account");
        }

        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser users = db.AspNetUsers.Find(id);
   
            if (users == null)
            {
                return HttpNotFound();
            }

            return View(users);
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSales_Invoice orders = db.tblSales_Invoice.Find(id);
       
            if (orders == null)
            {
                return HttpNotFound();
            }

            return View(orders);
        }
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}