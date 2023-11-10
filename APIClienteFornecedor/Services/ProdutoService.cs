using System.Collections.Generic;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Repository;

namespace APIClienteFornecedor.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IEnumerable<Produto> GetProdutos(ProdutoQueryParameters queryParameters)
        {
            return _produtoRepository.GetProdutos(queryParameters);
        }
    }
}
