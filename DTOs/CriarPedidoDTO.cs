namespace Sprint_3.DTOs
{
    public class CriarPedidoDTO
    {
        public int UsuarioId { get; set; }
        public List<CriarPedidoProdutoDTO> Produtos { get; set; } = new List<CriarPedidoProdutoDTO>();


    }
}
