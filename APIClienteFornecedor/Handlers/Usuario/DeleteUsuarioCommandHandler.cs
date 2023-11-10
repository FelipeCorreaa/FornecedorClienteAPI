using Npgsql;
using System;
using APIClienteFornecedor.Queries;
using System.Data;

namespace APIClienteFornecedor.Handlers.Usuario
{
    public class DeleteUsuarioCommandHandler
    {
        private readonly string _connectionString;

        public DeleteUsuarioCommandHandler()
        {
            ConfigureConnection configureConnection = new ConfigureConnection();
            _connectionString = configureConnection.GetConnectionString();
        }

        public string ExcluirUsuario(string UserName)
        {
            // Verificar se o usuário com o UserName especificado existe
            IConsultarUsuarioQueryHandler consultarUsuarioQueryHandler = new ConsultarUsuarioQueryHandler();
            if (!consultarUsuarioQueryHandler.UsuarioExiste(UserName))
            {
                return "Usuário não encontrado.";
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM Usuario WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    cmd.ExecuteNonQuery();
                }
            }

            return "Usuário excluído com sucesso.";
        }
    }
}
