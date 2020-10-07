using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Models;
using ToDoApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ToDoApp.Controllers
{
    public class AccountController : Controller
    {
        private EFDBContext context;
        public AccountController(EFDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAndRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                User user = await context.User.FirstOrDefaultAsync(u => u.Email == model.loginModel.Email && u.Password == model.loginModel.Password);
                if(user !=null)
                {
                    await Authenticate(model.loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин или пароль");
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginAndRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await context.User.FirstOrDefaultAsync(u => u.Email == model.registerModel.Email);
                if (user == null)
                {
                    context.User.Add(new Entity.User { Email = model.registerModel.Email, Name = model.registerModel.Name, Password = model.registerModel.Password });
                    await context.SaveChangesAsync();

                    await Authenticate(model.registerModel.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            else ModelState.AddModelError("", "Некорректные логин или пароль");
            return View(model);
        }

        private async Task Authenticate(string user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
