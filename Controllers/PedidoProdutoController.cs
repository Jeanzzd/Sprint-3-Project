using Microsoft.AspNetCore.Mvc;
using Sprint_3.DTOs;
using Sprint_3.Repository.Interface;

namespace Sprint_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoProdutoController : ControllerBase
    {
        private readonly IPedidoProduto _pedidoProdutoRepository;

        public PedidoProdutoController(IPedidoProduto pedidoProdutoRepository)
        {
            _pedidoProdutoRepository = pedidoProdutoRepository;
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoProdutoDTO>>> GetPedidosProdutos()
        {
            var pedidosProdutos = await _pedidoProdutoRepository.GetPedidosProdutos();

            if (pedidosProdutos == null || !pedidosProdutos.Any())
                return NotFound("Nenhum pedido-produto encontrado.");

            return Ok(pedidosProdutos);
        }
    }
}
