namespace APIClienteFornecedor.Commands.Produto
{
    public class DeletarProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int SupplierId { get; set; }
    }
}
