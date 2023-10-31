using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UploadFilesProject.Models;
using UploadFilesProject.Models.ViewModels;
using UploadFilesProject.Repositories.Interfaces;

namespace UploadFilesProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var result = await _userRepository.Register(registerViewModel.Name, registerViewModel.LastName, registerViewModel.UserName, registerViewModel.Email, registerViewModel.PasswordHash);
                if (result != null)
                {
                    return RedirectToAction("Index"); //Para probar si el usuario se esta registrando

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en el registro.");
                    return View(registerViewModel);

                }
            }
            else
            {
                return View(registerViewModel);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _userRepository.Login(loginViewModel.UserName, loginViewModel.PasswordHash);
                if (result != null)
                {
                    return RedirectToAction("Index"); //Para probar si el usuario se esta registrando

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error en el registro.");
                    return View(loginViewModel);

                }
            }
            else
            {
                return View(loginViewModel);
            }
        }
    }

}

