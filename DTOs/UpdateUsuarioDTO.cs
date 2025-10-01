using System.ComponentModel.DataAnnotations;

namespace Sprint_3.DTOs
{
    public class UpdateUsuarioDTO
    {
       
        public string Nome { get; set; } = string.Empty;

       
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
