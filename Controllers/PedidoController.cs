using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprint_3.DTOs;
using Sprint_3.Repository.Interface;

namespace Sprint_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _pedidoRepository;

        public PedidoController(IPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Retorna todos os pedidos com seus produtos
        /// </summary>
        /// <returns>Lista de pedidos</returns>
        /// <response code="200">Retorna todos os pedidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetPedidos();
            return Ok(pedidos);
        }

        /// <summary>
        /// Retorna um pedido pelo Id
        /// </summary>
        /// <param name="id">Id do pedido</param>
        /// <returns>Pedido</returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="404">Pedido não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> GetPedido(int id)
        {
            var pedido = await _pedidoRepository.GetPedido(id);
            if (pedido == null) return NotFound($"Pedido com Id {id} não encontrado.");
            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="dto">Dados do pedido</param>
        /// <remarks>
        /// Exemplo de payload:
        /// 
        ///     POST /api/Pedido
        ///     {
        ///        "usuarioId": 1,
        ///        "produtos": [
        ///            {
        ///                "produtoId": 101,
        ///                "quantidade": 2
        ///            },
        ///            {
        ///                "produtoId": 202,
        ///                "quantidade": 1
        ///            }
        ///        ]
        ///     }
        /// </remarks>
        /// <returns>Pedido criado</returns>
        /// <response code="201">Pedido criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// 
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> AdicionarPedido([FromBody] CriarPedidoDTO dto)
        {
            if (dto == null)
                return BadRequest("O pedido não pode ser nulo.");

            if (dto.UsuarioId <= 0)
                return BadRequest("O UsuarioId deve ser maior que zero.");

            if (dto.Produtos == null || !dto.Produtos.Any())
                return BadRequest("Os produtos do pedido não podem estar vazios.");

            if (dto.Produtos.Any(p => p.ProdutoId <= 0))
                return BadRequest("O ProdutoId de cada produto deve ser maior que zero.");

            if (dto.Produtos.Any(p => p.Quantidade <= 0))
                return BadRequest("A quantidade de cada produto deve ser maior que zero.");

            var pedido = await _pedidoRepository.AdicionarPedido(dto);
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        /// <summary>
        /// Atualiza o valor total de um pedido
        /// </summary>
        /// <param name="id">Id do pedido</param>
        /// <param name="dto">Dados para atualização</param>
        /// <returns>Pedido atualizado</returns>
        /// <response code="200">Pedido atualizado com sucesso</response>
        /// <response code="404">Pedido não encontrado</response>
        /// 
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoDTO>> AtualizarPedido(int id, [FromBody] UpdatePedidoDTO dto)
        {
            var pedido = await _pedidoRepository.AtualizarPedido(id, dto);
            if (pedido == null) return NotFound($"Pedido com Id {id} não encontrado.");
            return Ok(pedido);
        }

        /// <summary>
        /// Deleta um pedido
        /// </summary>
        /// <param name="id">Id do pedido</param>
        /// <returns>Pedido deletado</returns>
        /// <response code="200">Pedido deletado com sucesso</response>
        /// <response code="404">Pedido não encontrado</response>
        /// 
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PedidoDTO>> DeletarPedido(int id)
        {
            var pedido = await _pedidoRepository.DeletarPedido(id);
            if (pedido == null) return NotFound($"Pedido com Id {id} não encontrado.");
            return Ok(pedido);
        }
    }
}
