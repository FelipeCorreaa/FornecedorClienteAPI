using Npgsql;
using System;
using System.Data;

namespace APIClienteFornecedor.Queries
{
    public class ConsultarUsuarioQueryHandler : IConsultarUsuarioQueryHandler
    {
        private readonly string _connectionString;

        public ConsultarUsuarioQueryHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public bool UsuarioExiste(string UserName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Usuario WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}
