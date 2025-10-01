using System.ComponentModel.DataAnnotations;

namespace Sprint_3.Models
{
    public class Pedidos
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public int ValorTotal { get; set; } = 0;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
