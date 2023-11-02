using Microsoft.AspNetCore.Identity;
using UploadFilesProject.Models;
using UploadFilesProject.Repositories.Interfaces;
using UploadFilesProject.Utils;

namespace UploadFilesProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public AppUser GetUser()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<AppUser> Register(string name, string lastName, string userName, string email, string password)
        {
            var user = new AppUser()
            {
                Name = name,
                LastName = lastName,
                UserName = userName,
                Email = email,
                
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var roleExist = await _roleManager.RoleExistsAsync("User");
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
                return user;
            }
            throw new InvalidOperationException("La operación ha fallado. Detalles de error: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        }
    }
}