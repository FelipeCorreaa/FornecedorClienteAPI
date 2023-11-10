using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Queries;

namespace APIClienteFornecedor.Handlers.Usuario
{
    public class CriarUsuarioCommandHandler
    {
        private readonly string _connectionString;
        private readonly IConsultarUsuarioQueryHandler _consultarUsuarioQueryHandler;

        public CriarUsuarioCommandHandler(IConsultarUsuarioQueryHandler consultarUsuarioQueryHandler)
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
            _consultarUsuarioQueryHandler = consultarUsuarioQueryHandler;
        }

        public string CriarUsuario(string UserName, string Email, string Password, string AccountType)
        {
            // Verificar se o usuário já existe
            if (_consultarUsuarioQueryHandler.UsuarioExiste(UserName))
            {
                return "Usuário já existe.";
            }

            // Verificar se o AccountType é válido
            if (AccountType != "cliente" && AccountType != "fornecedor")
            {
                return "Só é permitido tipo de conta cliente ou fornecedor.";
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Usuario (UserName, Email, Password, account_type) VALUES (@UserName, @Email, @Password, @account_type) RETURNING id";
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@account_type", AccountType);

                    int userId = (int)cmd.ExecuteScalar();

                    // Agora, dependendo do tipo de conta, atualizamos a tabela correspondente
                    if (AccountType == "cliente")
                    {
                        cmd.CommandText = "INSERT INTO clients (user_id) VALUES (@UserId)";
                    }
                    else if (AccountType == "fornecedor")
                    {
                        cmd.CommandText = "INSERT INTO suppliers (user_id) VALUES (@UserId)";
                    }

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            return "Usuário inserido com sucesso.";
        }
    }
}
