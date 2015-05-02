using SimpleMemberShip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SimpleMemberShip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login loginData )
        {
           // return Content(loginData.Username + ' ' + loginData.Password);
            if (ModelState.IsValid)
            {
                if(WebSecurity.Login(loginData.Username, loginData.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login or password invalid");
                    return View(loginData);
                }
                
            }

            return View(loginData);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.Username, registerData.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException)
                {

                    ModelState.AddModelError("", "Sorry the username already exists");
                    return View(registerData);
                }
                

            }

            ModelState.AddModelError("", "Sorry the username already exists");
            return View(registerData);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}