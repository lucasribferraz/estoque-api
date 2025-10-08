using estoque_api.Models;

namespace estoque_api.Services.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria?> GetById(int id);
        Task<Categoria> AddCategoria(Categoria categoria);
        Task<Categoria?> UpdateCategoria(Categoria categoria);
        Task<Categoria?> RemoveCategoria(int id);
    }
}
