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
        private GartenkraftEntities dbContext = new GartenkraftEntities();

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
        [HttpPost]
        public ActionResult Add(vwProduct product, int? quantity)
        {
            bool isDuplicate = false;

            // check if cart is in session
            if (Session["Cart"] != null)
            {
                cart = (Cart)Session["Cart"]; // load cart if in session
                foreach(var i in cart.CartItems)
                {
                    if(i.product_id == product.product_id) { isDuplicate = true; } // set checker to see if product is already in cart
                }
            }

            // check to see if it is a duplicate line item
            if (isDuplicate)
            {
                // add 1 quantity to existing lineitem
                cart.CartItems.Where(ci => ci.product_id == product.product_id).Single().Set_lineitem_quantity(cart.CartItems.Where(ci => ci.product_id == product.product_id).Single().lineitem_quantity += 1);
            }
            else
            {
                // set product
                product.SetPriceRange();
                product.SetOption();

                // set line item
                invoice_lineitem li = new invoice_lineitem();
                li.SetProduct(product);
                li.Set_lineitem_quantity(Convert.ToInt32(quantity));

                // add to cart
                cart.AddItem(li);
            }

            // put cart in session
            Session["Cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        // GET: Cart/UpdateQuantity
        public ActionResult UpdateQuantity(invoice_lineitem item)
        {
            cart = (Cart)Session["Cart"];
            cart.UpdateItem(item);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        // GET: Cart/RemoveLineitem
        public ActionResult RemoveLineitem(int productID)
        {
            cart = (Cart)Session["Cart"];
            cart.RemoveItem(productID);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}