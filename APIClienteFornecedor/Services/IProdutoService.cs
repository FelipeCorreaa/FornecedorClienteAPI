using System.Collections.Generic;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Repository;

namespace APIClienteFornecedor.Services
{
    public interface IProdutoService
    {
        IEnumerable<Produto> GetProdutos(ProdutoQueryParameters queryParameters);
    }

}

