using LoggedAndLogouted.BancoContext;
using LoggedAndLogouted.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoggedAndLogouted.Service
{
    public class UserService : IUserInterface
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<LoginModel>>> CreateUser(LoginModel novoUser)
        {
            ServiceResponse<List<LoginModel>> serviceResponse = new ServiceResponse<List<LoginModel>>();

            try
            {
                if (novoUser == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                novoUser.DataDeCadastro = DateTime.Now.ToLocalTime();

                _context.Add(novoUser);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Usuarios.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<LoginModel>>> DeleteUser(int id)
        {
            ServiceResponse<List<LoginModel>> serviceResponse = new ServiceResponse<List<LoginModel>>();

            try
            {
                LoginModel user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }


                _context.Usuarios.Remove(user);
                await _context.SaveChangesAsync();


                serviceResponse.Dados = _context.Usuarios.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ReadLoginModel>>> GetLogin([FromBody] ReadLoginModel readLoginModel)
        {
            ServiceResponse<List<ReadLoginModel>> serviceResponse = new ServiceResponse<List<ReadLoginModel>>();

            try
            {

                var user = await _context.Usuarios
                    .Where(c => c.Login.ToLower() == readLoginModel.Login.ToLower() && c.Senha == readLoginModel.Senha)
                    .ToListAsync();

                if (user == null || user.Count == 0)
                {

                    serviceResponse.Mensagem = "Usuário ou senha inválidos!";
                    serviceResponse.Sucesso = false;
                    serviceResponse.Dados = null;
                }
                else
                {

                    serviceResponse.Dados = user.Select(c => new ReadLoginModel
                    {
                        Login = c.Login,
                        Senha = null
                    }).ToList();
                    serviceResponse.Mensagem = "Login realizado com sucesso!";
                    serviceResponse.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = "Erro: " + ex.Message;
                serviceResponse.Sucesso = false;
                serviceResponse.Dados = null;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<LoginModel>>> GetUser()
        {
            ServiceResponse<List<LoginModel>> serviceResponse = new ServiceResponse<List<LoginModel>>();

            try
            {

                serviceResponse.Dados = _context.Usuarios.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }


            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<LoginModel>> GetUserById(int id)
        {
            ServiceResponse<LoginModel> serviceResponse = new ServiceResponse<LoginModel>();

            try
            {
                LoginModel colaborador = _context.Usuarios.FirstOrDefault(x => x.Id == id);

                if (colaborador == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = colaborador;

            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
