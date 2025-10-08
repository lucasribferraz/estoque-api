using estoque_api.Models;

namespace estoque_api.Services.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> GetAllProdutos();
        public Task<Produto?> GetByIdProduto(int id);
        public Task<Produto> AddProduto(Produto produto);
        public Task<Produto?> UpdateProduto(Produto produto);
        public Task<Produto?> DeleteProduto(int id);
    }
}
