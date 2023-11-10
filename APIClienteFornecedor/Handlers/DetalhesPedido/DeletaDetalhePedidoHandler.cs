using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.DetalhePedido;

namespace APIClienteFornecedor.Handlers.DetalhePedido
{
    public class DeletaDetalhePedidoHandler
    {
        private readonly string _connectionString;

        public DeletaDetalhePedidoHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public void DeletarDetalhePedido(int PedidoId, int ProdutoId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM order_details WHERE order_id = @PedidoId AND product_id = @ProdutoId";
                    cmd.Parameters.AddWithValue("@PedidoId", PedidoId);
                    cmd.Parameters.AddWithValue("@ProdutoId", ProdutoId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
