using Microsoft.EntityFrameworkCore;
using Sprint_3.Data;
using Sprint_3.DTOs;
using Sprint_3.Models;
using Sprint_3.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprint_3.Repository
{
    public class UsuarioRepository : IUsuario
    {
        private readonly dbContext _context;

        public UsuarioRepository(dbContext context)
        {
            _context = context;
        }

     
        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            return await _context.Usuario
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    QuantidadePedidos = u.Pedidos.Count
                })
                .ToListAsync();
        }

        // Retorna um usuário por Id
        public async Task<UsuarioDTO> GetUsuario(int id)
        {
            var usuario = await _context.Usuario
                .Include(u => u.Pedidos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return null!;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                QuantidadePedidos = usuario.Pedidos.Count
            };
        }

     
        public async Task<UsuarioDTO> AdicionarUsuario(CriarUsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                QuantidadePedidos = 0
            };
        }

     
        public async Task<UsuarioDTO> AtualizarUsuario(int id, UpdateUsuarioDTO dto)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return null!;

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;

            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                QuantidadePedidos = usuario.Pedidos.Count
            };
        }

        public async Task<UsuarioDTO> DeletarUsuario(int id)
        {
            var usuario = await _context.Usuario
                .Include(u => u.Pedidos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return null!;

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                QuantidadePedidos = usuario.Pedidos.Count
            };


        }

        public async Task<Usuario?> Login(string email)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
