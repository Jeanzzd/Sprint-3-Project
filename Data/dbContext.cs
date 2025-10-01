using Microsoft.EntityFrameworkCore;
using Sprint_3.Models;

namespace Sprint_3.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<PedidoProduto> PedidoProdutos { get; set; } 
        public DbSet<Pedidos> Pedidos { get; set; } 
        public DbSet<Usuario> Usuario { get; set; } 
        public DbSet<Produtos> Produtos { get; set; }

    }
}
