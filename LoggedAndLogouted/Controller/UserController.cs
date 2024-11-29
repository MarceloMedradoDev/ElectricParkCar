using LoggedAndLogouted.Models;
using LoggedAndLogouted.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoggedAndLogouted.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpPost("CriarUser")]
        public async Task<ActionResult<ServiceResponse<List<LoginModel>>>> CreateColaborador(LoginModel novoUser)
        {
            return Ok(await _userInterface.CreateUser(novoUser));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<LoginModel>>>> GetUser()
        {
            return Ok(await _userInterface.GetUser());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<LoginModel>>> GetUserById(int id)
        {
            ServiceResponse<LoginModel> serviceResponse = await _userInterface.GetUserById(id);

            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<LoginModel>>>> DeleteUser(int id)
        {
            ServiceResponse<List<LoginModel>> serviceResponse = await _userInterface.DeleteUser(id);

            return Ok(serviceResponse);

        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<List<ReadLoginModel>>>> GetLogins([FromBody] ReadLoginModel readLoginModel)
        {
            return Ok(await _userInterface.GetLogin(readLoginModel));
        }

    }
}
