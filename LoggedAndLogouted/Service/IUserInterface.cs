using LoggedAndLogouted.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoggedAndLogouted.Service
{
    public interface IUserInterface
    {
        Task<ServiceResponse<List<LoginModel>>> CreateUser(LoginModel novoUser);
        Task<ServiceResponse<LoginModel>> GetUserById(int id);
        Task<ServiceResponse<List<LoginModel>>> GetUser();
        Task<ServiceResponse<List<LoginModel>>> DeleteUser(int id);
        Task<ServiceResponse<List<ReadLoginModel>>> GetLogin([FromBody] ReadLoginModel readLoginModel);
    }
}
