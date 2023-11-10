namespace APIClienteFornecedor.Models
{
    public class ProdutoQueryParameters
    {
        public string Nome { get; set; }
        public decimal? PrecoMin { get; set; }
        public decimal? PrecoMax { get; set; }
        public string OrdenarPor { get; set; }
        public string Ordem { get; set; }
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
    }

}
