using Microsoft.EntityFrameworkCore;
using Sprint_3.Data;
using Sprint_3.DTOs;
using Sprint_3.Models;
using Sprint_3.Repository.Interface;

namespace Sprint_3.Repository
{
    public class PedidoRepository : IPedido
    {
        private readonly dbContext _context;

        public PedidoRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<PedidoDTO> AdicionarPedido(CriarPedidoDTO dto)
        {
         
            if (dto.UsuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            if (dto.Produtos == null || !dto.Produtos.Any())
                throw new ArgumentException("É necessário informar pelo menos um produto.");

          
            var pedido = new Pedidos
            {
                UsuarioId = dto.UsuarioId,
                DataPedido = DateTime.Now,
                ValorTotal = 0
            };

        
            foreach (var item in dto.Produtos)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {item.ProdutoId} não encontrado.");

                
                pedido.PedidoProdutos.Add(new PedidoProduto
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade
                });

            
                pedido.ValorTotal += produto.Preco * item.Quantidade;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            
            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Produtos = new List<PedidoProdutoDTO>()
            };

        
            foreach (var pp in pedido.PedidoProdutos)
            {
                var produto = await _context.Produtos.FindAsync(pp.ProdutoId);
                pedidoDTO.Produtos.Add(new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = produto.Nome,
                    PrecoProduto = produto.Preco,
                    Quantidade = pp.Quantidade
                });
            }

            return pedidoDTO;
        }

     
        public async Task<PedidoDTO> AtualizarPedido(int id, UpdatePedidoDTO dto)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return null!;

            pedido.ValorTotal = dto.ValorTotal;

            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Produtos = pedido.PedidoProdutos.Select(pp => new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = pp.Produto.Nome,
                    PrecoProduto = pp.Produto.Preco,
                    Quantidade = pp.Quantidade
                }).ToList()
            };

            return pedidoDTO;
        }

     
        public async Task<PedidoDTO> DeletarPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return null!;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Produtos = pedido.PedidoProdutos.Select(pp => new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = pp.Produto.Nome,
                    PrecoProduto = pp.Produto.Preco,
                    Quantidade = pp.Quantidade
                }).ToList()
            };

            return pedidoDTO;
        }

      
        public async Task<PedidoDTO> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return null!;

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Produtos = pedido.PedidoProdutos.Select(pp => new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = pp.Produto.Nome,
                    PrecoProduto = pp.Produto.Preco,
                    Quantidade = pp.Quantidade
                }).ToList()
            };

            return pedidoDTO;
        }

       
        public async Task<IEnumerable<PedidoDTO>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .ToListAsync();

            return pedidos.Select(pedido => new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Produtos = pedido.PedidoProdutos.Select(pp => new PedidoProdutoDTO
                {
                    ProdutoId = pp.ProdutoId,
                    NomeProduto = pp.Produto.Nome,
                    PrecoProduto = pp.Produto.Preco,
                    Quantidade = pp.Quantidade
                }).ToList()
            }).ToList();
        }
    }
}
