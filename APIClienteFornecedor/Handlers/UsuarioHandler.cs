using global::APIClienteFornecedor.Commands;
using global::APIClienteFornecedor.Data;
using global::APIClienteFornecedor.Models;
using global::APIClienteFornecedor.Queries;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace APIClienteFornecedor.Handlers
{


    public class UsuarioHandler
    {
        private readonly ApplicationDbContext _context;

        public UsuarioHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CriarUsuarioCommand command)
        {
            var usuario = new Usuario
            {
                UserName = command.UserName,
                Email = command.Email
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> Handle(ObterUsuariosQuery query)
        {
            // Lógica para obter usuários (pode ser mais complexa dependendo das necessidades)
            return await _context.Usuarios.ToListAsync();
        }
    }
}

