namespace APIClienteFornecedor.Commands.Usuario
{
    public class CriarUsuarioCommand
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string account_type { get; set; }
    }

}
