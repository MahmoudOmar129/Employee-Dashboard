using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {



        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        #region Registration


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                IsAgree = model.IsAgree
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);

        }

        #endregion


        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");
            }

            return View(model);
        }

        #endregion


        #region Sign Out

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                // Email -  Token 
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                // localhost://Account/ResetPassword/Elgendyz@gmai.com+dsgdbvdvdfber43434t4t34g43g
                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                // MailSender.Mail("Password Reset", passwordResetLink);
                //logger.Log(LogLevel.Warning, passwordResetLink);

                EventLog log = new EventLog();
                log.Source = "Hr System";
                log.WriteEntry(passwordResetLink, EventLogEntryType.Information);

                return RedirectToAction("ConfirmForgetPassword");
            }

            return View(model);
        }

        #endregion


        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmResetPassword");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }



        #endregion

    }
}
