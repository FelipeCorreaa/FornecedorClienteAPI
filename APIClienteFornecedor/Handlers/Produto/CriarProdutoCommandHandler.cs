using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Queries;

namespace APIClienteFornecedor.Handlers.Produto
{
    public class CriarProdutoCommandHandler
    {
        private readonly string _connectionString;

        public CriarProdutoCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public int CriarProduto(string Nome, decimal Preco, int SupplierId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO products (name, price, supplier_id) VALUES (@Name, @Price, @SupplierId) RETURNING id";
                    cmd.Parameters.AddWithValue("@Name", Nome);
                    cmd.Parameters.AddWithValue("@Price", Preco);
                    cmd.Parameters.AddWithValue("@SupplierId", SupplierId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
