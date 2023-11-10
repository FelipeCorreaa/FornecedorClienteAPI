using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands;

namespace APIClienteFornecedor.Handlers
{
    public class CriarUsuarioCommandHandler
    {
        private readonly string _connectionString;

        public CriarUsuarioCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public void criarusurio(string UserName, string Email)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Usuario (UserName, Email) VALUES (@UserName, @Email)";
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Email", Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
