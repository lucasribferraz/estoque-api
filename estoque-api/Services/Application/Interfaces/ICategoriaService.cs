using estoque_api.Models.DTO;

namespace estoque_api.Services.Application.Interfaces
{
    public interface ICategoriaService
    {
        public Task<IEnumerable<CategoriaDTO>> GetAllCategorias();
        public Task<CategoriaDTO> GetByIdCategoria(int id);
        public Task<CategoriaDTO> AddCategoria(CategoriaDTO categoriaDTO);
        public Task<CategoriaDTO> UpdateCategoria(CategoriaDTO categoriaDTO, int id);
        public Task DeleteCategoria(int id);
    }
}
