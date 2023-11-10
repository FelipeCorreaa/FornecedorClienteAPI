using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands.Pedido;

namespace APIClienteFornecedor.Handlers.Pedido
{
    public class DeletarPedidoCommandHandler
    {
        private readonly string _connectionString;

        public DeletarPedidoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string DeletarPedido(int Id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM orders WHERE id = @Id";
                    cmd.Parameters.AddWithValue("@Id", Id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Pedido excluído com sucesso.";
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
