using Npgsql;
using System;

namespace APIClienteFornecedor.Services
{
    public class UpdateSuppliersService
    {
        private readonly string _connectionString;

        public UpdateSuppliersService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AtualizarFornecedores()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO suppliers (user_id) SELECT id FROM usuario";

                    int rowsAffected = cmd.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} fornecedores adicionados.");
                }
            }
        }
    }
}
