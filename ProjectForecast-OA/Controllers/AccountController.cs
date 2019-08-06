using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using ProjectForecast_OA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjectForecast_OA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            EFCodeFirstDbContext context = new EFCodeFirstDbContext();
            var valid_users = context.Consultants.Select(x=>x).Where(user => user.Consultant_Name == model.Name && user.PassWord == model.Password).FirstOrDefault();
            ViewData["Users"] = valid_users;
            //var login_user = valid_users.FirstOrDefault(x => model.Name.Equals(x.Account) && model.Password.Equals(x.Password));
            if (valid_users != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, valid_users.Consultant_Name));
                claims.Add(new Claim(ClaimTypes.Name, valid_users.PassWord));

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddMonths(1)
                }, identity);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "登录不合法！");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Signup(string returnUrl)
        {
            EFCodeFirstDbContext context = new EFCodeFirstDbContext();

            ViewBag.Countries = context.Country.Select(x => x.CountryName).ToList();
            //ViewBag.Roles = context.Role.Select(x => x).ToList();
            ViewBag.Roles = new List<string> { "Manager", "Consultant", "Dev" };
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Signup(SignupViewModel model, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "注册不合法！");
                }
                EFCodeFirstDbContext context = new EFCodeFirstDbContext();

                Consultant user = new Consultant()
                {
                    Consultant_Name=model.Name,
                    PassWord=model.Password,
                    Country=model.Country,
                    Role=model.Role
                };
                context.Consultants.Add(user);
                context.SaveChanges();
                return RedirectToLocal(returnUrl);
            }
            catch
            {
                ModelState.AddModelError("", "注册不合法！");
                return View(model);
            }
            
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private string AbsoluteDataFile(string file)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Int", file);
        }

        private T CovertJsonToDocument<T>(string jsonFile)
        {
            string jsonDataText = System.IO.File.ReadAllText(jsonFile);
            var data = JsonConvert.DeserializeObject<T>(jsonDataText);

            return data;
        }
    }
}