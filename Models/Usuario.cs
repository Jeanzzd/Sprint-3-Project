using System.ComponentModel.DataAnnotations;

namespace Sprint_3.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
  
        public int QuantidadePedidos => Pedidos.Count;
    }
}
