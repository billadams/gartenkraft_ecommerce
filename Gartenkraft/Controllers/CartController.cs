using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gartenkraft.Models;

namespace Gartenkraft.Controllers
{
    public class CartController : Controller
    {
        private Cart cart = new Cart();
        private GartenkraftCustomerEntities dbContext = new GartenkraftCustomerEntities();

        // GET: Cart
        public ActionResult Index()
        {
            // check if cart is in session
            if (Session["Cart"] != null)
            {
                cart = (Cart)Session["Cart"]; // load cart if in session
            }
            return View(cart);
        }

        // GET: Cart/AddToCart
        public ActionResult AddToCart(int productID)
        {
            bool isDuplicate = false;

            // check if cart is in session
            if (Session["Cart"] != null)
            {
                cart = (Cart)Session["Cart"]; // load cart if in session
                foreach(var i in cart.CartItems)
                {
                    if(i.product_id == productID) { isDuplicate = true; } // set checker to see if product is already in cart
                }
            }

            // check to see if it is a duplicate line item
            if (isDuplicate)
            {
                // add 1 quantity to existing lineitem
                cart.CartItems.Where(ci => ci.product_id == productID).Single().lineitem_quantity += 1;
            }
            else
            {
                // set product
                var product = dbContext.vwProducts.Where(vP => vP.product_id == productID).Single();

                // set line item
                tblSales_Invoice_Lineitem li = new tblSales_Invoice_Lineitem();
                li.SetProduct(product);
                li.lineitem_quantity = 1;

                // add to cart
                cart.AddItem(li);
            }

            // put cart in session
            Session["Cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity([Bind(Include = "lineitem_quantity, product_id")] tblSales_Invoice_Lineitem item)
        {
            cart = (Cart)Session["Cart"];
            cart.CartItems.Where(ci => ci.product_id == item.product_id).Single().lineitem_quantity = item.lineitem_quantity;
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        // GET: Cart/RemoveLineitem
        public ActionResult RemoveLineitem(int productID)
        {
            cart = (Cart)Session["Cart"];
            cart.CartItems.Remove(cart.CartItems.Where(ci => ci.product_id == productID).Single());
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}