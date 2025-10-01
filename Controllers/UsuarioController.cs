using Microsoft.AspNetCore.Mvc;
using Sprint_3.DTOs;
using Sprint_3.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna a lista de usuários</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetUsuarios();
            return Ok(usuarios);
        }



        /// <summary>
        /// Retorna um usuário pelo Id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Dados do usuário</returns>
        /// <response code="200">Usuário encontrado</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuario(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário a ser criado</param>
        /// <remarks>
        /// Exemplo de payload:
        ///
        ///     POST /api/Usuario
        ///     {
        ///        "nome": "João Silva",
        ///        "email": "joao.silva@email.com",
        ///        "senha": "123456"
        ///     }
        ///
        /// </remarks>
        /// <returns>Usuário criado</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> AdicionarUsuario([FromBody] CriarUsuarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados do usuário inválidos.");

            var usuarioCriado = await _usuarioRepository.AdicionarUsuario(dto);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCriado.Id }, usuarioCriado);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="dto">Dados atualizados do usuário</param>
        /// <returns>Usuário atualizado</returns>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> AtualizarUsuario(int id, [FromBody] UpdateUsuarioDTO dto)
        {
            var usuarioAtualizado = await _usuarioRepository.AtualizarUsuario(id, dto);
            if (usuarioAtualizado == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuarioAtualizado);
        }

        /// <summary>
        /// Deleta um usuário pelo Id.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Usuário deletado</returns>
        /// <response code="200">Usuário deletado com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioDTO>> DeletarUsuario(int id)
        {
            var usuarioDeletado = await _usuarioRepository.DeletarUsuario(id);
            if (usuarioDeletado == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuarioDeletado);
        }
    }
}
