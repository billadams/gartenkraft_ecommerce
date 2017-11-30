using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Areas.Admin.Controllers.AdminControllers
{
    public class ProductCategoriesAdminController : Controller
    {
        private GartenkraftEntities db = new GartenkraftEntities();

        // GET: Product_Category
        public ActionResult Index()
        {
            var tblProduct_Category = db.tblProduct_Category.Include(t => t.tblProduct_Line);
            return View(tblProduct_Category.ToList());
        }

        // GET: Product_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Category);
        }

        // GET: Product_Category/Create
        public ActionResult Create()
        {
            ViewBag.category_product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name");
            return View();
        }

        // POST: Product_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,category_product_line_id,category_image_id,category_name,soft_delete,is_visible")] tblProduct_Category tblProduct_Category)
        {
            if (ModelState.IsValid)
            {
                db.tblProduct_Category.Add(tblProduct_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct_Category.category_product_line_id);
            return View(tblProduct_Category);
        }

        // GET: Product_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct_Category.category_product_line_id);
            return View(tblProduct_Category);
        }

        // POST: Product_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_product_line_id,category_image_id,category_name,soft_delete,is_visible")] tblProduct_Category tblProduct_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProduct_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_product_line_id = new SelectList(db.tblProduct_Line, "product_line_id", "product_line_name", tblProduct_Category.category_product_line_id);
            return View(tblProduct_Category);
        }

        // GET: Product_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            if (tblProduct_Category == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct_Category);
        }

        // POST: Product_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct_Category tblProduct_Category = db.tblProduct_Category.Find(id);
            db.tblProduct_Category.Remove(tblProduct_Category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
