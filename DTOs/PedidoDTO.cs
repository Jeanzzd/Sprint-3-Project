namespace Sprint_3.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public int UsuarioId { get; set; }

        // Lista de produtos no pedido
        public List<PedidoProdutoDTO> Produtos { get; set; } = new();
    }
}
