using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.ViewModel;

namespace WebStore.controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UM;
        private readonly SignInManager<User> _SM;

        public AccountController(UserManager<User> UM, SignInManager<User> SM)
        {
            _UM = UM;
            _SM = SM;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var login_result = await _SM.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if(login_result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Имя пользователя или пароль неверны");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var new_user = new User { UserName = model.UserName };

            var create_result = await _UM.CreateAsync(new_user, model.Password);
            if(create_result.Succeeded)
            {
                await _SM.SignInAsync(new_user, false);

                return RedirectToAction("Index", "Home");
            }
            foreach (var error in create_result.Errors)
                ModelState.AddModelError("",error.Description);
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _SM.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
