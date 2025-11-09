using Sprint_3.DTOs;
using Sprint_3.Models;
using System.Collections;

namespace Sprint_3.Repository.Interface
{
    public interface IUsuario
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuario(int id);
        Task<UsuarioDTO> AdicionarUsuario(CriarUsuarioDTO usuario);
        Task<UsuarioDTO> AtualizarUsuario(int id, UpdateUsuarioDTO usuario);
        Task<UsuarioDTO> DeletarUsuario(int id);

        Task<Usuario?> Login(string email);
    }
}
