using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.DetalhePedido;

namespace APIClienteFornecedor.Handlers.DetalhePedido
{
    public class AtualizaDetalhePedidoHandler
    {
        private readonly string _connectionString;

        public AtualizaDetalhePedidoHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public void AtualizarDetalhePedido(int PedidoId, int ProdutoId, int Quantidade)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE order_details SET quantity = @Quantidade WHERE order_id = @PedidoId AND product_id = @ProdutoId";
                    cmd.Parameters.AddWithValue("@PedidoId", PedidoId);
                    cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);
                    cmd.Parameters.AddWithValue("@Quantidade", Quantidade);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
