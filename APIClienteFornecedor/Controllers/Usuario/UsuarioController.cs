﻿using APIClienteFornecedor.Commands.APIClienteFornecedor.Commands;
using APIClienteFornecedor.Commands.Usuario;
using APIClienteFornecedor.Handlers.Usuario;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;

namespace APIClienteFornecedor.Controllers.Usuario
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Pooling=true;Database=ClientesFornecedoresAPI;User Id=postgres;Password=1234";
        private readonly CriarUsuarioCommandHandler _criarUsuarioHandler;
        private readonly AtualizarUsuarioCommandHandler _atualizarUsuarioHandler;
        private readonly DeleteUsuarioCommandHandler _deleteUsuarioHandler;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(
            IAuthenticationService authenticationService,
            CriarUsuarioCommandHandler criarUsuarioHandler,
            AtualizarUsuarioCommandHandler atualizarUsuarioHandler,
            DeleteUsuarioCommandHandler deleteUsuarioHandler)
        {
            _authenticationService = authenticationService;
            _criarUsuarioHandler = criarUsuarioHandler;
            _atualizarUsuarioHandler = atualizarUsuarioHandler;
            _deleteUsuarioHandler = deleteUsuarioHandler;
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
                    string mensagem = _criarUsuarioHandler.CriarUsuario(usuario.UserName, usuario.Email, usuario.Password, usuario.account_type);
                    return Ok(mensagem);
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

        [HttpPut("{UserName}")]
        public IActionResult PutUsuario(string UserName, AtualizarUsuarioCommand atualizarUsuarioCommand)
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
                    string mensagem = _atualizarUsuarioHandler.AtualizarUsuario(atualizarUsuarioCommand.Id, atualizarUsuarioCommand.UserName, atualizarUsuarioCommand.Email, atualizarUsuarioCommand.Password, atualizarUsuarioCommand.account_type, UserName);
                    return Ok(mensagem);
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

        [HttpDelete("{UserName}")]
        public IActionResult DeleteUsuario(string UserName)
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
                    string mensagem = _deleteUsuarioHandler.ExcluirUsuario(UserName);
                    return Ok(mensagem);
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
