using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Handlers;
using APIClienteFornecedor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;
using System.Text;
using APIClienteFornecedor.Services;

namespace APIClienteFornecedor.Controllers.Usuario
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Pooling=true;Database=ClientesFornecedoresAPI;User Id=postgres;Password=1234";
        private readonly CriarUsuarioCommandHandler _usuarioHandler;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IAuthenticationService authenticationService, CriarUsuarioCommandHandler usuarioHandler)
        {
            _authenticationService = authenticationService;
            _usuarioHandler = usuarioHandler;
        }

        [HttpPost]
        public IActionResult PostUsuario(CriarUsuarioCommand usuario)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Unauthorized("Token de autenticação não fornecido.");
            }

            if (authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring(7);

                string storedToken = _authenticationService.ReadTokenFromFile();

                if (token == storedToken)
                {
                    _usuarioHandler.criarusurio(usuario.UserName, usuario.Email);
                    return Ok();
                }
                else
                {
                    return Unauthorized("Token inválido.");
                }
            }
            else
            {
                return Unauthorized("Esquema de autenticação inválido. Utilize o formato 'Bearer'.");
            }
        }
    }
}
