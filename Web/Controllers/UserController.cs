using System;
using System.Web.Mvc;
using System.Web.Security;

using AutoMapper;

using Domain.Models;

using OnlineJudge.Service.Interfaces;
using OnlineJudge.Web.ViewModels;

namespace OnlineJudge.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (this.IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return RedirectToAction("Home", "Home");
                }

                this.ModelState.AddModelError("", "Invalid login credentials");
            }

            return this.View(user);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Registration(UserRegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(userRegistrationViewModel);
                var crypto = new SimpleCrypto.PBKDF2();
                var encryptedPassword = crypto.Compute(userRegistrationViewModel.Password);

                user.PasswordSalt = crypto.Salt;
                user.Password = encryptedPassword;

                userService.AddUser(user);

                return RedirectToAction("Home", "Home");
            }

            return this.View(userRegistrationViewModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Home", "Home");
        }

        private bool IsValid(string email, string password)
        {
            var user = userService.GetUser(email);

            if (user == null)
            {
                return false;
            }

            var crypto = new SimpleCrypto.PBKDF2();

            return user.Password == crypto.Compute(password, user.PasswordSalt);
        }
    }
}
