using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UploadFilesProject.Data;
using UploadFilesProject.Models;
using UploadFilesProject.Repositories.Interfaces;

namespace UploadFilesProject.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FileRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task AddFile(int id, string fileName, byte[] FileData, string userId)
        {
            var userFile = new UserFile()
            {
                Id = id,
                FileName = fileName,
                FileData = FileData,
                UserId = userId
            };
        
            if (userFile != null)
            {
                _context.UserFiles.Add(userFile);
                await _context.SaveChangesAsync();
            }
        }
        

        public async Task DeleteFile(int id)
        {
            var file = _context.UserFiles.Find(id); 
            if (file != null)
            {
                _context.UserFiles.Remove(file);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El elemento con el ID especificado no existe en la base de datos.");
            }
        }

        public Task<UserFile> GetFile(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserFile> GetFiles(string userId)
        {
            return _context.UserFiles.Where(uf => uf.UserId == userId);
        }

        public async Task UpdateFile(UserFile oUserFile)
        {
            var file = _context.UserFiles.FirstOrDefault(uf=>uf.Id == oUserFile.Id);
            if(file != null)
            {
                file.FileName = oUserFile.FileName;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("El elemento con el ID especificado no existe en la base de datos.");
            }
        }
    }
}
