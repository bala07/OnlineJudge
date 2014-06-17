using System;
using System.Web.Mvc;
using System.Web.Security;

using Domain.Models;

using OnlineJudge.Service;
using OnlineJudge.Service.Interfaces;
using OnlineJudge.Web.Models;

namespace OnlineJudge.Web.Controllers
{
    [HandleError(View = "Error")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController()
        {
            userService = new UserService();
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
        public ActionResult Login(UserLoginViewModel user)
        {


            if (ModelState.IsValid)
            {
                if (this.IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
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
                var crypto = new SimpleCrypto.PBKDF2();

                var encryptedPassword = crypto.Compute(userRegistrationViewModel.Password);

                var user = new User
                               {
                                   Email = userRegistrationViewModel.Email,
                                   Password = encryptedPassword,
                                   PasswordSalt = crypto.Salt,
                                   Username = userRegistrationViewModel.Username
                               };

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
