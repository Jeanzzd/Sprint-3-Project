using Microsoft.AspNetCore.Mvc;
using Sprint_3.DTOs;
using Sprint_3.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprint_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoRepository;

        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos</returns>
        /// <response code="200">Produtos retornados com sucesso</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetProdutos();
            return Ok(produtos);
        }

        /// <summary>
        /// Retorna um produto pelo Id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto encontrado</returns>
        /// <response code="200">Produto retornado com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var produto = await _produtoRepository.GetProduto(id);
            if (produto == null) return NotFound("Produto não encontrado.");
            return Ok(produto);
        }


        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        /// <param name="dto">Dados do produto</param>
        /// <remarks>
        /// Exemplo de payload:
        /// 
        ///     POST /api/Produto
        ///     {
        ///        "nome": "Camiseta",
        ///        "preco": 79.90
        ///     }
        /// </remarks>
        /// <returns>Produto criado</returns>
        /// <response code="201">Produto criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> AdicionarProduto([FromBody] CriarProdutoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nome) || dto.Preco < 0)
                return BadRequest("Dados do produto inválidos.");

            var produto = await _produtoRepository.AdicionarProduto(dto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <param name="dto">Dados para atualização</param>
        /// <returns>Produto atualizado</returns>
        /// <response code="200">Produto atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Produto não encontrado</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDTO>> AtualizarProduto(int id, [FromBody] UpdateProdutoDTO dto)
        {
            if (dto == null) return BadRequest("Dados do produto inválidos.");

            var produtoAtualizado = await _produtoRepository.AtualizarProduto(id, dto);
            if (produtoAtualizado == null) return NotFound("Produto não encontrado.");

            return Ok(produtoAtualizado);
        }

        /// <summary>
        /// Deleta um produto pelo Id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto deletado</returns>
        /// <response code="200">Produto deletado com sucesso</response>
        /// <response code="404">Produto não encontrado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> DeletarProduto(int id)
        {
            var produtoDeletado = await _produtoRepository.DeletarProduto(id);
            if (produtoDeletado == null) return NotFound("Produto não encontrado.");

            return Ok(produtoDeletado);
        }
    }
}
