namespace Sprint_3.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Apenas o número de pedidos ou lista resumida
        public int QuantidadePedidos { get; set; }
        public List<PedidoDTO>? Pedidos { get; set; }
    }
}
