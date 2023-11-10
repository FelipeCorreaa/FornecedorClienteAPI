using Npgsql;
using System;
using System.Data;
using APIClienteFornecedor.Commands;
using APIClienteFornecedor.Queries;

namespace APIClienteFornecedor.Handlers.Usuario
{
    public class AtualizarUsuarioCommandHandler
    {
        private readonly string _connectionString;

        public AtualizarUsuarioCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string AtualizarUsuario(int id, string UserName, string Email, string Password, string AccountType, string nomeAntigo)
        {
            // Verificar se o usuário com o ID especificado existe
            IConsultarUsuarioQueryHandler consultarUsuarioQueryHandler = new ConsultarUsuarioQueryHandler();
            if (!consultarUsuarioQueryHandler.UsuarioExiste(UserName))
            {
                return "Usuário não encontrado.";
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
                    cmd.CommandText = "UPDATE Usuario SET UserName = @UserName, Email = @Email, Password = @Password, account_type = @account_type WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@account_type", AccountType);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }

            return "Usuário atualizado com sucesso.";
        }
    }
}
