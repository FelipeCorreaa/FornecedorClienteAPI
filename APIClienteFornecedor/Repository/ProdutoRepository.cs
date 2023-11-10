using System.Linq;
using APIClienteFornecedor.Data;
using APIClienteFornecedor.Models;
using APIClienteFornecedor.Services;
using Microsoft.EntityFrameworkCore;

namespace APIClienteFornecedor.Repository
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetProdutos(ProdutoQueryParameters queryParameters);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> GetProdutos(ProdutoQueryParameters queryParameters)
        {
            // Lógica para consultar o banco de dados com base nos parâmetros de consulta
            // Utilize o LINQ ou uma ferramenta de construção de consultas para criar a consulta
            var minhaQuery = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters.Nome))
            {
                minhaQuery = minhaQuery.Where(p => p.Nome.Contains(queryParameters.Nome));
            }

            if (queryParameters.PrecoMin.HasValue)
            {
                minhaQuery = minhaQuery.Where(p => p.Preco >= queryParameters.PrecoMin.Value);
            }

            if (queryParameters.PrecoMax.HasValue)
            {
                minhaQuery = minhaQuery.Where(p => p.Preco <= queryParameters.PrecoMax.Value);
            }

            // Adicione mais lógica conforme necessário

            // Aplicar ordenação
            if (!string.IsNullOrEmpty(queryParameters.OrdenarPor))
            {
                minhaQuery = minhaQuery.OrderByDynamic(queryParameters.OrdenarPor, queryParameters.Ordem == "descendente");
            }

            // Aplicar paginação
            var produtosPaginados = minhaQuery
                .Skip((queryParameters.Pagina - 1) * queryParameters.TamanhoPagina)
                .Take(queryParameters.TamanhoPagina)
                .ToList();

            return produtosPaginados;
        }
    }
}
