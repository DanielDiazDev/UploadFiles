using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UploadFilesProject.Models;
using UploadFilesProject.Models.ViewModels;
using UploadFilesProject.Repositories;
using UploadFilesProject.Repositories.Interfaces;

namespace UploadFilesProject.Controllers
{
    [Authorize(Roles = "User")]
    public class FileController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public FileController(IFileRepository fileRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile([FromForm]UploadViewModel uploadViewModel) //MEJORAR Y AGREGAR LOS DEMAS METODOS
        {
            var response = new ResponseBase();
            var file = uploadViewModel.FormFile;
            try
            {
                if (file != null && file.Length > 0)
                {
                    int id = 0;
                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    var fileData = stream.ToArray();
                    var fileName = file.FileName;
                    var currentUser = await _userManager.GetUserAsync(User);
                    string userId = currentUser.Id;
                    response.Message = "Archivo guardado correctamente";
                    response.Ok = true;
                    await _fileRepository.AddFile(id, fileName, fileData, userId);
                }

                
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Json(response);
        }
        [HttpGet]
        public async Task<JsonResult> GetFiles()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            string userId = currentUser.Id;
            var listOfFiles = _fileRepository.GetFiles(userId).ToList();
            return Json(listOfFiles);
        }
        [HttpDelete]
        public async Task<JsonResult> DeleteFile([FromBody]DeleteFileViewModel deleteFileViewModel)
        {
            var response = new ResponseBase();
            try
            {
                response.Message = "Archivo eliminado correctamente";
                response.Ok = true;
                await _fileRepository.DeleteFile(deleteFileViewModel.Id);

            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Json(response);
        }
        [HttpPut]
        public async Task<JsonResult> ModifiedFile([FromBody] ModifiedFileViewModel modifiedFileViewModel)
        {
            var response = new ResponseBase();
            try
            {
                var file = _mapper.Map<UserFile>(modifiedFileViewModel);
                response.Message = "Archivo modificado correctamente";
                response.Ok = true;
                await _fileRepository.UpdateFile(file);

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
