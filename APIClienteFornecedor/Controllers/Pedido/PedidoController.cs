using Microsoft.AspNetCore.Mvc;
using APIClienteFornecedor.Commands.Pedido;
using APIClienteFornecedor.Handlers.Pedido;
using Microsoft.AspNetCore.Authorization;
using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Commands.Produto;
using APIClienteFornecedor.Handlers.Produto;
using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.APIClienteFornecedor.Commands;
using APIClienteFornecedor.Commands.Usuario;
using APIClienteFornecedor.Handlers.Usuario;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Services;
using APIClienteFornecedor.Commands.Produto;
namespace APIClienteFornecedor.Controllers.Pedido
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly CriarPedidoCommandHandler _criarPedidoHandler;
        private readonly AtualizarPedidoCommandHandler _atualizarPedidoHandler;
        private readonly DeletarPedidoCommandHandler _deletarPedidoHandler;
        private readonly IAuthenticationService _authenticationService;

        public PedidoController(
            CriarPedidoCommandHandler criarPedidoHandler,
            AtualizarPedidoCommandHandler atualizarPedidoHandler,
            DeletarPedidoCommandHandler deletarPedidoHandler,
            IAuthenticationService authenticationService)
        {
            _criarPedidoHandler = criarPedidoHandler;
            _atualizarPedidoHandler = atualizarPedidoHandler;
            _deletarPedidoHandler = deletarPedidoHandler;
            _authenticationService = authenticationService;
        }

        private bool ValidateToken(string token)
        {
            string storedToken = _authenticationService.ReadTokenFromFile();
            return token == storedToken;
        }

        [HttpPost]
      
        public IActionResult CriarPedido(CriarPedido pedido)
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
                    int pedidoId = _criarPedidoHandler.CriarPedido(pedido.ClienteId, pedido.DataPedido);
                    return Ok($"Pedido criado com ID {pedidoId}");
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

        [HttpPut("{Id}")]
       
        public IActionResult AtualizarPedido(int Id, AtualizarPedido atualizarPedido)
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
                    string mensagem = _atualizarPedidoHandler.AtualizarPedido(Id, atualizarPedido.ClienteId, atualizarPedido.DataPedido);

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

        [HttpDelete("{Id}")]
        
        public IActionResult DeletarPedido(int Id)
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
                    string mensagem = _deletarPedidoHandler.DeletarPedido(Id);

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
