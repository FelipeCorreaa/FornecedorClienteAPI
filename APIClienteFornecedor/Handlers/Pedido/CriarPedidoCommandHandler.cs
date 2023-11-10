using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.Pedido;

namespace APIClienteFornecedor.Handlers.Pedido
{
    public class CriarPedidoCommandHandler
    {
        private readonly string _connectionString;

        public CriarPedidoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public int CriarPedido(int ClienteId, DateTime DataPedido)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO orders (client_id, order_date) VALUES (@ClienteId, @DataPedido) RETURNING id";
                    cmd.Parameters.AddWithValue("@ClienteId", ClienteId);
                    cmd.Parameters.AddWithValue("@DataPedido", DataPedido);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
