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
        public async Task AddFile(int id, string filename, string contentTipe, byte[] data, string userId)
        {
            var userFile = new UserFile()
            {
                Id = id,
                FileName = filename,
                ContentType = contentTipe,
                Data = data,
                UserId = userId
            };
        
            if (userFile != null)
            {
                _context.UserFiles.Add(userFile);
                await _context.SaveChangesAsync();
            }
        }
        

        public Task DeleteFile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserFile> GetFile(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public Task UpdateFile(UserFile oUserFile)
        {
            throw new NotImplementedException();
        }
    }
}
