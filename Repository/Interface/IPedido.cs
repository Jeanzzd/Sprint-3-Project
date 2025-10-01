using Sprint_3.DTOs;

namespace Sprint_3.Repository.Interface
{
    public interface IPedido
    {
        Task<IEnumerable<PedidoDTO>> GetPedidos();                 
        Task<PedidoDTO> GetPedido(int id);                    
        Task<PedidoDTO> AdicionarPedido(CriarPedidoDTO dto);
        Task<PedidoDTO> AtualizarPedido(int id, UpdatePedidoDTO dto);
        Task<PedidoDTO> DeletarPedido(int id);
    }
}
