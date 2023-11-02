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
        public async Task<JsonResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var response = new ResponseBase();

            try
            {
                var result = await _userRepository.Register(registerViewModel.Name, registerViewModel.LastName, registerViewModel.UserName, registerViewModel.Email, registerViewModel.PasswordHash);

                if (result != null)
                {
                    response.Message = "Usuario guardado correctamente";
                    response.Ok = true;
                }
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Json(response);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login([FromBody] LoginViewModel loginViewModel)
        {


            var response = new ResponseBase();

            try
            {
                var result = await _userRepository.Login(loginViewModel.UserName, loginViewModel.PasswordHash);

                if (result != null)
                {
                    response.Message = "Usuario cargado correctamente";
                    response.Ok = true;
                }
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Json(response);


        }
    }

}
