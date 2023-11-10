using Microsoft.AspNetCore.Mvc;
using APIClienteFornecedor.Commands.DetalhePedido;
using APIClienteFornecedor.Handlers.DetalhePedido;
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

namespace APIClienteFornecedor.Controllers.Pedido
{
    [ApiController]
    [Route("[controller]")]
    public class DetalhePedidoController : ControllerBase
    {
        private readonly CriaDetalhePedidoHandler _criaDetalhePedidoHandler;
        private readonly AtualizaDetalhePedidoHandler _atualizaDetalhePedidoHandler;
        private readonly DeletaDetalhePedidoHandler _deletaDetalhePedidoHandler;
        private readonly IAuthenticationService _authenticationService;

        public DetalhePedidoController(
            CriaDetalhePedidoHandler criaDetalhePedidoHandler,
            AtualizaDetalhePedidoHandler atualizaDetalhePedidoHandler,
            DeletaDetalhePedidoHandler deletaDetalhePedidoHandler,
            IAuthenticationService authenticationService)
        {
            _criaDetalhePedidoHandler = criaDetalhePedidoHandler;
            _atualizaDetalhePedidoHandler = atualizaDetalhePedidoHandler;
            _deletaDetalhePedidoHandler = deletaDetalhePedidoHandler;
            _authenticationService = authenticationService;
        }

        private bool ValidateToken(string token)
        {
            string storedToken = _authenticationService.ReadTokenFromFile();
            return token == storedToken;
        }

        [HttpPost]
  
        public IActionResult CriarDetalhePedido(CriaDetalhePedido detalhePedido)
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
                    _criaDetalhePedidoHandler.CriarDetalhePedido(detalhePedido.PedidoId, detalhePedido.ProdutoId, detalhePedido.Quantidade);

                    return Ok("Detalhe do pedido criado com sucesso.");
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

        [HttpPut("{PedidoId}/{ProdutoId}")]
 
        public IActionResult AtualizarDetalhePedido(int PedidoId, int ProdutoId, AtualizaDetalhePedido atualizaDetalhePedido)
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
                    _atualizaDetalhePedidoHandler.AtualizarDetalhePedido(PedidoId, ProdutoId, atualizaDetalhePedido.Quantidade);

                    return Ok("Detalhe do pedido atualizado com sucesso.");
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

        [HttpDelete("{PedidoId}/{ProdutoId}")]
 
        public IActionResult DeletarDetalhePedido(int PedidoId, int ProdutoId)
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
                    _deletaDetalhePedidoHandler.DeletarDetalhePedido(PedidoId, ProdutoId);

                    return Ok("Detalhe do pedido deletado com sucesso.");
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
