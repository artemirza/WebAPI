using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserManagerWrapper : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagerWrapper(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return _userManager.AddToRoleAsync(user, role);
        }
    }
}
