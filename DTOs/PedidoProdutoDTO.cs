namespace Sprint_3.DTOs
{
    public class PedidoProdutoDTO
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal PrecoProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
