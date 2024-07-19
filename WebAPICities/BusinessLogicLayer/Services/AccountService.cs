using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserManager _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public AccountService(IUserManager userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string?> LoginAsync(DTO.Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var token = _tokenGenerator.Generate(user, userRoles);

                return token;
            }

            return null;
        }

        public async Task<bool> RegisterAsync(DTO.Register model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                return roleResult.Succeeded;
            }

            return false;
        }


        public async Task<bool> AssignRoleAsync(DTO.UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded;
        }

    }
}
