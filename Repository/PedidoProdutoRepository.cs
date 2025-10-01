using Microsoft.EntityFrameworkCore;
using Sprint_3.Data;
using Sprint_3.DTOs;
using Sprint_3.Repository.Interface;
using System.Collections;

namespace Sprint_3.Repository
{
    public class PedidoProdutoRepository : IPedidoProduto
    {
        private readonly dbContext _context;

        public PedidoProdutoRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoProdutoDTO>> GetPedidosProdutos()
        {
            // Faz o join entre PedidoProduto e Produtos para pegar as informações necessárias
            var pedidosProdutos = await _context.PedidoProdutos
                .Include(pp => pp.Produto) // Inclui os dados do produto
                .Select(pp => new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = pp.Produto.Nome,
                    PrecoProduto = pp.Produto.Preco,
                    Quantidade = pp.Quantidade
                })
                .ToListAsync();

            return pedidosProdutos;
        }
    }
}
