using Sprint_3.DTOs;
using Sprint_3.Models;
using System.Collections;

namespace Sprint_3.Repository.Interface
{
    public interface IProduto
    {
        Task<IEnumerable<ProdutoDTO>> GetProdutos();             // lista de produtos
        Task<ProdutoDTO> GetProduto(int id);                     // produto único
        Task<ProdutoDTO> AdicionarProduto(CriarProdutoDTO dto); // criar
        Task<ProdutoDTO> AtualizarProduto(int id, UpdateProdutoDTO dto); // atualizar
        Task<ProdutoDTO> DeletarProduto(int id);

    }
}
