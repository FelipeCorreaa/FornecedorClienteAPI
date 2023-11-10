using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Queries;

namespace APIClienteFornecedor.Handlers.Produto
{
    public class DeleteProdutoCommandHandler
    {
        private readonly string _connectionString;

        public DeleteProdutoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string ExcluirProduto(string Nome)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM products WHERE name = @Nome";
                    cmd.Parameters.AddWithValue("@Nome", Nome);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Produto excluído com sucesso.";
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
