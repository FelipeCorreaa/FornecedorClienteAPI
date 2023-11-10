using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Commands.Produto;
using APIClienteFornecedor.Handlers.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.APIClienteFornecedor.Commands;
using APIClienteFornecedor.Commands.Usuario;
using APIClienteFornecedor.Handlers.Usuario;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Services;
using APIClienteFornecedor.Commands.Produto;



namespace APIClienteFornecedor.Controllers.Produto
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly CriarProdutoCommandHandler _criarProdutoHandler;
        private readonly AtualizarProdutoCommandHandler _atualizarProdutoHandler;
        private readonly DeleteProdutoCommandHandler _deleteProdutoHandler;
        private readonly IAuthenticationService _authenticationService;

        public ProdutoController(
            CriarProdutoCommandHandler criarProdutoHandler,
            AtualizarProdutoCommandHandler atualizarProdutoHandler,
            DeleteProdutoCommandHandler deleteProdutoHandler,
            IAuthenticationService authenticationService)
        {
            _criarProdutoHandler = criarProdutoHandler;
            _atualizarProdutoHandler = atualizarProdutoHandler;
            _deleteProdutoHandler = deleteProdutoHandler;
            _authenticationService = authenticationService;
        }

        private bool ValidateToken(string token)
        {
            string storedToken = _authenticationService.ReadTokenFromFile();
            return token == storedToken;
        }

        [HttpPost]

        public IActionResult PostProduto(CriarProdutoCommand produto)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Unauthorized("Token de autenticação não fornecido.");
            }

            if (authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring(7);

                if (ValidateToken(token))
                {
                    int produtoId = _criarProdutoHandler.CriarProduto(produto.Nome, produto.Preco, produto.SupplierId);




                    return Ok(produtoId);
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

        [HttpPut("{Nome}")]

        public IActionResult PutProduto(string Nome, AtualizarProduto atualizarProdutoCommand)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Unauthorized("Token de autenticação não fornecido.");
            }

            if (authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring(7);

                if (ValidateToken(token))
                {
                    string mensagem = _atualizarProdutoHandler.AtualizarProduto(Nome, atualizarProdutoCommand.Preco, atualizarProdutoCommand.SupplierId);
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

        [HttpDelete("{Nome}")]

        public IActionResult DeleteProduto(string Nome)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Unauthorized("Token de autenticação não fornecido.");
            }

            if (authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring(7);

                if (ValidateToken(token))
                {
                    string mensagem = _deleteProdutoHandler.ExcluirProduto(Nome);
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
