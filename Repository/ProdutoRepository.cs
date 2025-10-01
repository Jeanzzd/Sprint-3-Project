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
    public class ProdutoRepository : IProduto
    {
        private readonly dbContext _context;

        public ProdutoRepository(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            return await _context.Produtos
                .Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco
                }).ToListAsync();
        }

        public async Task<ProdutoDTO> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null) return null!; 

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public async Task<ProdutoDTO> AdicionarProduto(CriarProdutoDTO dto)
        {
            if (dto.Nome == null || dto.Nome.Trim() == "")
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (dto.Preco < 0)
                throw new ArgumentException("O preço do produto não pode ser negativo.");

            var produto = new Produtos
            {
                Nome = dto.Nome,
                Preco = dto.Preco
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public async Task<ProdutoDTO> AtualizarProduto(int id, UpdateProdutoDTO dto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return null!; // ou lançar exceção

            if (!string.IsNullOrWhiteSpace(dto.Nome))
                produto.Nome = dto.Nome;

            if (dto.Preco >= 0)
                produto.Preco = dto.Preco;

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public async Task<ProdutoDTO> DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return null!; // ou lançar exceção

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
    }
}
