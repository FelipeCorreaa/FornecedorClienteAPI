using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.DetalhePedido;

namespace APIClienteFornecedor.Handlers.DetalhePedido
{
    public class CriaDetalhePedidoHandler
    {
        private readonly string _connectionString;

        public CriaDetalhePedidoHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public void CriarDetalhePedido(int PedidoId, int ProdutoId, int Quantidade)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO order_details (order_id, product_id, quantity) VALUES (@PedidoId, @ProdutoId, @Quantidade)";
                    cmd.Parameters.AddWithValue("@PedidoId", PedidoId);
                    cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);
                    cmd.Parameters.AddWithValue("@Quantidade", Quantidade);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
