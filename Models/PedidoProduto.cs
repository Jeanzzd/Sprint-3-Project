using System.ComponentModel.DataAnnotations;

namespace Sprint_3.Models
{
    public class PedidoProduto
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public Pedidos Pedido { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produtos Produto { get; set; } = null!;

        // Quantidade de cada produto no pedido
        public int Quantidade { get; set; }

    }
}
