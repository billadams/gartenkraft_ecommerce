﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Gartenkraft.Models;
using Gartenkraft.ViewModels;
using Gartenkraft;
using Gartenkraft.Controllers;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net;

namespace Gartenkraft.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            ViewBag.StatusMessage = "";
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
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
                //return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                ViewBag.StatusMessage = "Your password has been succesfully changed";
                return View();
            }
            AddErrors(result);
            return View(model);
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

#region Roles

        public ActionResult ManageRoles()
        {
            var roles = RoleManager.Roles.ToList();
            var roleModels = new List<RoleViewModel>();
            foreach (var i in roles) { roleModels.Add(new RoleViewModel() { Id = i.Id, Name = i.Name }); }
            return View(roleModels);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                // validation for if role exist
                if (RoleManager.RoleExists(role.Name))
                {
                    ViewBag.ErrorMessage = "Role name already exist";
                    return View(role);
                }
                RoleManager.Create(new IdentityRole() { Name = role.Name });
            }
            return RedirectToAction("ManageRoles");
        }

        public ActionResult EditRole(string id)
        {
            var editRole = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            if (editRole == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var editRoleModel = new RoleViewModel() { Id = editRole.Id, Name = editRole.Name };
            return View(editRoleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                RoleManager.Update(new IdentityRole() { Id = role.Id, Name = role.Name });
                return RedirectToAction("ManageRoles");
            }
            return View(role);
        }

        public ActionResult DeleteRole(string id)
        {
            var deleteRole = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            if (deleteRole == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deleteRoleModel = new RoleViewModel() { Id = deleteRole.Id, Name = deleteRole.Name };
            return View(deleteRoleModel);
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleConfirm(string id)
        {
            var deleteRole = RoleManager.Roles.SingleOrDefault(r => r.Id == id);
            if (deleteRole == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleManager.Delete(deleteRole);
            return RedirectToAction("ManageRoles");
        }

        #endregion

#region Profile & Admins

        //public ActionResult MakeAdmin()
        //{
        //    UserManager.AddToRole(User.Identity.GetUserId(), "Admin");
        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult ManageAdmins()
        {
            var roleID = RoleManager.FindByName("Admin").Id;
            var admins = UserManager.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == roleID) != null).ToList();

            return View(admins);
        }

        public ActionResult CreateAdmin()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAdmin(AdminViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var userID = UserManager.FindByName(user.UserName).Id;
                var roleResult = UserManager.AddToRole(userID, "Admin");
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("ManageAdmins", "Manage");
                }
            }
            // If we got this far, something failed, redisplay form and delete the newly created user
            var deleteUser = UserManager.FindByName(user.UserName);
            UserManager.Delete(deleteUser);
            ViewBag.ErrorMessage = "Something went wrong. Please contact your administrator. Unable to create an admin.";
            return View(model);
        }

        public ActionResult DeleteAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var admin = UserManager.FindById(id);
            var adminModel = new AdminViewModel() {
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
                FirstName = admin.FirstName,
                LastName = admin.LastName
            };
            ViewBag.ID = id;
            return View(adminModel);
        }

        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminConfirm(string id)
        {
            UserManager.RemoveFromRole(id, "Admin");
            var deleteUser = UserManager.FindById(id);
            UserManager.Delete(deleteUser);
            return RedirectToAction("ManageAdmins");
        }

        public ActionResult ManageProfile()
        {
            var userID = User.Identity.GetUserId();
            var userProfile = UserManager.Users.FirstOrDefault(u => u.Id == userID);
            if (userProfile == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ErrorMessage = "";
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProfile(ApplicationUser user)
        {
            ViewBag.ErrorMessage = "Unable to update your profile. Please try again.";
            if (ModelState.IsValid)
            {
                var newUser = UserManager.Users.SingleOrDefault(u => u.Id == user.Id);
                if (newUser == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                newUser.Email = user.Email;
                newUser.UserName = user.Email;
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.Address1 = user.Address1;
                newUser.Address2 = user.Address2;
                newUser.City = user.City;
                newUser.State = user.State;
                newUser.Zip = user.Zip;
                newUser.Zip4 = user.Zip4;
                newUser.Country = user.Country;
                newUser.PhoneNumber = user.PhoneNumber;

                UserManager.Update(newUser);
                ViewBag.ErrorMessage = "Profile was succesfully updated.";
            }
            ViewBag.States = XmlHelper.GetStates(Server, Url);
            ViewBag.Countries = XmlHelper.GetCountries(Server, Url);
            return View(user);
        }

#endregion

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