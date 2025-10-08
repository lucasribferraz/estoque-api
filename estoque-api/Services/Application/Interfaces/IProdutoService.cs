using estoque_api.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Services.Application.Interfaces
{
    public interface IProdutoService
    {
        public Task<IEnumerable<ProdutoDTO>> GetAllProdutos();
        public Task<ProdutoDTO> GetByIdProduto(int id);
        public Task<ProdutoDTO> AddProduto(ProdutoDTO produtoDTO);
        public Task<ProdutoDTO> UpdateProduto(ProdutoDTO produtoDTO, int id);
        public Task DeleteProduto(int id);
    }
}
