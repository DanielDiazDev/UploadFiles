using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UploadFilesProject.Models;
using UploadFilesProject.Repositories.Interfaces;

namespace UploadFilesProject.Controllers
{
    [Authorize(Roles = "User")]
    public class FileController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly UserManager<AppUser> _userManager;

        public FileController(IFileRepository fileRepository, UserManager<AppUser> userManager)
        {
            _fileRepository = fileRepository;
            _userManager = userManager;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null)
            {
                // Realiza la lógica para obtener los componentes necesarios del archivo, como el ID, nombre de archivo, tipo de contenido y datos.
                int id = 0; // Establece el ID adecuado.
                string filename = file.FileName;
                string contentType = file.ContentType;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] data = memoryStream.ToArray();

                    // Obtiene el usuario actual
                    var currentUser = await _userManager.GetUserAsync(User);

                    if (currentUser != null)
                    {
                        string userId = currentUser.Id; // Obtiene el ID del usuario.

                        // Llama al método AddFile en el repositorio para guardar el archivo en la base de datos.
                        await _fileRepository.AddFile(id, filename, contentType, data, userId);
                        return RedirectToAction("Index"); // Redirige a una página de éxito o donde desees.
                    }
                    else
                    {
                        // Maneja el caso en el que el usuario actual no se pudo obtener.
                        return RedirectToAction("Error"); // Redirige a una página de error o donde desees.
                    }
                }
            }

            // Maneja el caso en el que no se seleccionó un archivo.
            ModelState.AddModelError("file", "Please select a file.");
            return View();
        }

    }
}
