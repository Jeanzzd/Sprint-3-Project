using Sprint_3.DTOs;
using System.Collections;

namespace Sprint_3.Repository.Interface
{
    public interface IPedidoProduto
    {
        Task<IEnumerable<PedidoProdutoDTO>> GetPedidosProdutos();              
    }
}
