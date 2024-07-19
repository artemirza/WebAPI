using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login = BusinessLogicLayer.DTO.Login;
using Register = BusinessLogicLayer.DTO.Register;


namespace WebAPICities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Register request)
        {

            var result = await _accountService.RegisterAsync(request);
            if (!result)
            {
                return BadRequest("User registration failed.");
            }
            return Ok("User registered successfully");

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login request)
        {
            var token = await _accountService.LoginAsync(request);
            if (token == null)
            {
                return Unauthorized("Login failed");
            }
            return Ok(new { Token = token });
        }

        [Authorize(Roles = RoleConstants.AdminRole)]
        [HttpPost("Assign-Role")]
        public async Task<IActionResult> AssignRole(UserRole request)
        {
            var result = await _accountService.AssignRoleAsync(request);
            if (!result)
            {
                return BadRequest("User not found or role assignment failed.");
            }
            return Ok("Role assigned successfully");
        }
    }
}
