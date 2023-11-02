using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadFilesProject.Models;

namespace UploadFilesProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        AppUser GetUser();
        Task<AppUser> Register(string name, string lastName, string userName, string email, string password);
        Task<AppUser> Login(string usarName, string password);
    }
}
