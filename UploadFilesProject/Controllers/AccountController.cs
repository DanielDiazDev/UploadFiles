using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register([FromBody]RegisterViewModel registerViewModel)
        {
            var response = new ResponseBase(); //Crear ResponseBase
            try
            {
                if (ModelState.IsValid)
                {
                    response.Message = "Usuario guardado correctamente";
                    var result = await _userRepository.Register(registerViewModel.Name, registerViewModel.LastName, registerViewModel.UserName, registerViewModel.Email, registerViewModel.PasswordHash);
                    if (result != null)
                    {
                        //return RedirectToAction("Index", "File"); Buscar como redireccionar con json
                    }
                    else
                    {
                         ModelState.AddModelError(string.Empty, "Error en el registro.");
                          //  return View(registerViewModel);

                    }
                }
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Json(response);
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var result = await _userRepository.Register(registerViewModel.Name, registerViewModel.LastName, registerViewModel.UserName, registerViewModel.Email, registerViewModel.PasswordHash);
        //        if (result != null)
        //        {
        //            return RedirectToAction("Index","File"); //Para probar si el usuario se esta registrando

        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Error en el registro.");
        //            return View(registerViewModel);

        //        }
        //    }
        //    else
        //    {
        //        return View(registerViewModel);
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _userRepository.Login(loginViewModel.UserName, loginViewModel.PasswordHash);
                if (result != null)
                {
                    return RedirectToAction("Index", "File"); // Redirige al usuario a la página principal después del inicio de sesión exitoso.
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
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
