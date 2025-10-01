using System.ComponentModel.DataAnnotations;

namespace Sprint_3.Models
{
    public class Produtos
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Preco { get; set; } = 0;

        // Relacionamento N:N com Pedido
        public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
