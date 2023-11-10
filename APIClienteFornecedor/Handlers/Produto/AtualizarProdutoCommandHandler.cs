using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Queries;

namespace APIClienteFornecedor.Handlers.Produto
{
    public class AtualizarProdutoCommandHandler
    {
        private readonly string _connectionString;

        public AtualizarProdutoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string AtualizarProduto(string Nome, decimal NovoPreco, int NovoSupplierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE products SET price = @NovoPreco, supplier_id = @NovoSupplierId WHERE name = @Nome";
                    cmd.Parameters.AddWithValue("@Nome", Nome);
                    cmd.Parameters.AddWithValue("@NovoPreco", NovoPreco);
                    cmd.Parameters.AddWithValue("@NovoSupplierId", NovoSupplierId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Produto atualizado com sucesso.";
                    }
                    else
                    {
                        return "Produto não encontrado.";
                    }
                }
            }
        }
    }
}
