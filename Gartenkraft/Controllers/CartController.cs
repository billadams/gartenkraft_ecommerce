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
                    if(i.product_id == product.product_id && i.GetProduct().SelectedOptionID == product.SelectedOptionID) { isDuplicate = true; } // set checker to see if product is already in cart
                }
            }

            // check to see if it is a duplicate line item
            if (isDuplicate)
            {
                // add 1 quantity to existing lineitem
                cart.CartItems.Where(ci => ci.product_id == product.product_id && ci.GetProduct().SelectedOptionID == product.SelectedOptionID).Single().Set_lineitem_quantity(cart.CartItems.Where(ci => ci.product_id == product.product_id && ci.GetProduct().SelectedOptionID == product.SelectedOptionID).Single().lineitem_quantity += 1);
            }
            else
            {
                // set product option and price
                var newProduct = dbContext.vwProducts.FirstOrDefault(p => p.product_id == product.product_id);
                newProduct.SelectedOptionID = product.SelectedOptionID;
                newProduct.SetPrice();

                // set line item
                InvoiceLineItemTable li = new InvoiceLineItemTable();
                li.SetProduct(newProduct);
                li.Set_lineitem_quantity(Convert.ToInt32(quantity));

                // add to cart
                cart.AddItem(li);
            }

            // put cart in session
            Session["Cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        // GET: Cart/UpdateQuantity
        public ActionResult UpdateQuantity(InvoiceLineItemTable item)
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