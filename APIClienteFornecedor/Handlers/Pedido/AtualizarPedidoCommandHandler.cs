using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.Pedido;

namespace APIClienteFornecedor.Handlers.Pedido
{
    public class AtualizarPedidoCommandHandler
    {
        private readonly string _connectionString;

        public AtualizarPedidoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string AtualizarPedido(int Id, int ClienteId, DateTime DataPedido)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE orders SET client_id = @ClienteId, order_date = @DataPedido WHERE id = @Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ClienteId", ClienteId);
                    cmd.Parameters.AddWithValue("@DataPedido", DataPedido);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Pedido atualizado com sucesso.";
                    }
                    else
                    {
                        return "Pedido não encontrado.";
                    }
                }
            }
        }
    }
}
