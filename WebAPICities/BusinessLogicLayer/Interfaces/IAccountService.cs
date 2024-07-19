using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountService
    {
        Task<string?> LoginAsync(Login model);
        Task<bool> RegisterAsync(Register model);
        Task<bool> AssignRoleAsync(UserRole model);
    }
}
