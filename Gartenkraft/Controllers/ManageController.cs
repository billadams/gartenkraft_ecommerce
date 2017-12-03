using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Gartenkraft.Models;
using Gartenkraft.ViewModels;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;

namespace Gartenkraft.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private GartenkraftEntities db;

        public ManageController()
        {
            db = new GartenkraftEntities();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            db = new GartenkraftEntities();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        // partial profile view
        public PartialViewResult _UserProfile()
        {
            var db = new ApplicationDbContext();
            var id = User.Identity.GetUserId();
            var user = db.Users.Find(id);
            db.Dispose();
            return PartialView(user);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost, ActionName ("PartialChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        // ----------------------------------change password partial-------------------
        //
        // GET: /Manage/PartialChangePassword
        public PartialViewResult PartialChangePassword()
        {
            return PartialView();
        }

        // ----------------------------------change profile partial--------------------
        //
        // GET: /Manage/PartialChangeProfile
        public PartialViewResult PartialChangeProfile()
        {
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);

            var db = new ApplicationDbContext();
            var id = User.Identity.GetUserId();
            var user = db.Users.Find(id);
            db.Dispose();
            return PartialView(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialChangeProfile(ApplicationUser user)
        {
            var db = new ApplicationDbContext();

            // get user being edited and enter edited fields
            var editUser = db.Users.Find(user.Id);
            editUser.Email = user.Email;
            editUser.PhoneNumber = user.PhoneNumber;
            editUser.FirstName = user.FirstName;
            editUser.LastName = user.LastName;
            editUser.Address1 = user.Address1;
            editUser.Address2 = user.Address2;
            editUser.City = user.City;
            editUser.State = user.State;
            editUser.Zip = user.Zip;
            editUser.Zip4 = user.Zip4;
            editUser.Country = user.Country;

            // save to database
            db.Entry(editUser).State = System.Data.Entity.EntityState.Modified;
            var result = db.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", new { Message = "" });
            }
            db.Dispose();
            return RedirectToAction("Index", new { Message = ManageMessageId.Error });
        }

        // ----------------------------Partial Billing--------------------------------
        //
        // GET /Manage/PartialBilling
        public PartialViewResult PartialBilling()
        {
            var id = User.Identity.GetUserId();
            var userBillings = db.tblBilling_Information.Where(b => b.customer_id == id).ToList();
            return PartialView(userBillings);
        }

        // GET /Manage/PartialCreateBilling
        public PartialViewResult PartialCreateBilling()
        {
            var newBilling = new tblBilling_Information();
            newBilling.customer_id = User.Identity.GetUserId();
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return PartialView(newBilling);
        }

        // POST /Manage/PartialCreateBilling
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialCreateBilling(tblBilling_Information tblBilling_Information)
        {
            if (ModelState.IsValid)
            {
                if (db.tblBilling_Information.Where(b => b.billing_address1 == tblBilling_Information.billing_address1 
                                                         && b.billing_address2 == tblBilling_Information.billing_address2
                                                         && b.billing_city == tblBilling_Information.billing_city
                                                         && b.billing_state == tblBilling_Information.billing_state
                                                         && b.billing_zip == tblBilling_Information.billing_zip
                                                         && b.billing_zip4 == tblBilling_Information.billing_zip4
                                                         && b.billing_country == tblBilling_Information.billing_country).ToList().Count() == 0)
                {
                    db.tblBilling_Information.Add(tblBilling_Information);
                }
                
                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "This address had been recorded. Please enter a different address";
                    return PartialView("Error");
                }
            }

            var id = User.Identity.GetUserId();
            var billings = db.tblBilling_Information.Where(b => b.customer_id == id).ToList();
            return PartialView("PartialBilling", billings);
        }

        // GET: /Manage/PartialEditBilling
        public PartialViewResult PartialEditBilling(int? id)
        {
            var billing = db.tblBilling_Information.Find(id);
            if (billing == null)
            {
                ViewBag.ErrorMessage = "Unable to find your billing information.";
                return PartialView("Error");
            }
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return PartialView(billing);
        }

        // POST: /Manage/PartialEditBilling
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialEditBilling(tblBilling_Information tblBilling_Information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBilling_Information).State = System.Data.Entity.EntityState.Modified;
                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "Unable to modify your billing information. Please try again.";
                    return PartialView("Error");
                }
            }
            var id = User.Identity.GetUserId();
            var billings = db.tblBilling_Information.Where(b => b.customer_id == id).ToList();
            return PartialView("PartialBilling", billings);
        }

        // POST: /Manage/PartialDeleteBilling
        [HttpPost]
        public PartialViewResult PartialDeleteBilling(int? id)
        {
            if (id != null)
            {
                var billing = db.tblBilling_Information.Find(id);
                db.tblBilling_Information.Remove(billing);
                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "Unable to delete billing information. Please try again.";
                    return PartialView("Error");
                }
            }
            var userID = User.Identity.GetUserId();
            var billings = db.tblBilling_Information.Where(b => b.customer_id == userID).ToList();
            return PartialView("PartialBilling", billings);
        }


        // -----------------------------Partial Shipping---------------------------------
        //
        // GET /Manage/PartialShipping
        public PartialViewResult PartialShipping()
        {
            var id = User.Identity.GetUserId();
            var userShippings = db.tblShippings.Where(s => s.customer_id == id).ToList();
            return PartialView(userShippings);
        }

        // GET /Manage/PartialCreateShipping
        public PartialViewResult PartialCreateShipping()
        {
            var newShipping = new tblShipping();
            newShipping.customer_id = User.Identity.GetUserId();
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return PartialView(newShipping);
        }

        // POST /Manage/PartialCreateShipping
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialCreateShipping(tblShipping tblShipping)
        {
            if (ModelState.IsValid)
            {
                if (db.tblShippings.Where(b => b.shipping_address1 == tblShipping.shipping_address1
                                            && b.shipping_address2 == tblShipping.shipping_address2
                                            && b.shipping_city == tblShipping.shipping_city
                                            && b.shipping_state == tblShipping.shipping_state
                                            && b.shipping_zip == tblShipping.shipping_zip
                                            && b.shipping_zip4 == tblShipping.shipping_zip4
                                            && b.shipping_country == tblShipping.shipping_country).ToList().Count() == 0)
                {
                    db.tblShippings.Add(tblShipping);
                }

                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "This shipping address had been recorded. Please enter a different address";
                    return PartialView("Error");
                }
            }

            var id = User.Identity.GetUserId();
            var shippings = db.tblShippings.Where(b => b.customer_id == id).ToList();
            return PartialView("PartialShipping", shippings);
        }

        // GET: /Manage/PartialEditShipping
        public PartialViewResult PartialEditShipping(int? id)
        {
            var shipping = db.tblShippings.Find(id);
            if (shipping == null)
            {
                ViewBag.ErrorMessage = "Unable to find your shipping information.";
                return PartialView("Error");
            }
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return PartialView(shipping);
        }

        // POST: /Manage/PartialEditShipping
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialEditShipping(tblShipping tblShipping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblShipping).State = System.Data.Entity.EntityState.Modified;
                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "Unable to modify your shipping information. Please try again.";
                    return PartialView("Error");
                }
            }
            var id = User.Identity.GetUserId();
            var shippings = db.tblShippings.Where(b => b.customer_id == id).ToList();
            return PartialView("PartialShipping", shippings);
        }

        // POST: /Manage/PartialDeleteShipping
        [HttpPost]
        public PartialViewResult PartialDeleteShipping(int? id)
        {
            if (id != null)
            {
                var shipping = db.tblShippings.Find(id);
                db.tblShippings.Remove(shipping);
                var result = db.SaveChanges();
                if (result == 0)
                {
                    ViewBag.ErrorMessage = "Unable to delete shipping information. Please try again.";
                    return PartialView("Error");
                }
            }
            var userID = User.Identity.GetUserId();
            var shippings = db.tblShippings.Where(b => b.customer_id == userID).ToList();
            return PartialView("PartialShipping", shippings);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        // partial views to check if user has password
        public PartialViewResult ModifyPassword()
        {
            var model = new ModifyPasswordModel();
            model.HasPassword = HasPassword();
            return PartialView(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}