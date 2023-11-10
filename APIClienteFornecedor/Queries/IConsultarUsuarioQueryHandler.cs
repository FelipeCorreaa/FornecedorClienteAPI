namespace APIClienteFornecedor.Queries
{
    public interface IConsultarUsuarioQueryHandler
    {
        bool UsuarioExiste(string UserName);
    }
}
