using estoque_api.Data;
using estoque_api.Exceptions;
using estoque_api.Models;
using estoque_api.Models.DTO;
using estoque_api.Services.Application.Interfaces;
using estoque_api.Services.Repositories.EF_Core;
using estoque_api.Services.Repositories.Interfaces;
using estoque_api.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace estoque_api.Services.Application
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        private IValidator<CategoriaDTO> _validator;    
        public CategoriaService(IValidator<CategoriaDTO> validator, ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _validator = validator;
        }
        private async Task<Categoria> ObterCategoriaExistente(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                throw new CategoriaException("Categoria não encontrada");
            }
            return categoria;
        }
        public async Task<IEnumerable<CategoriaDTO>> GetAllCategorias()
        {
            var categorias = await _categoriaRepository.GetAllCategorias();
            var categoriasDTO = categorias.Select(c => new CategoriaDTO
            {
                Nome = c.Nome,
                Descricao = c.Descricao,
            }).ToList();
            return categoriasDTO;
        }
        public async Task<CategoriaDTO> GetByIdCategoria(int id)
        {
            var categoria = await ObterCategoriaExistente(id);
            var categoriaDTO = new CategoriaDTO
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
            };
            return categoriaDTO;
        }
        public async Task<CategoriaDTO> AddCategoria(CategoriaDTO categoriaDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(categoriaDTO);

            if (!result.IsValid)
                throw new CategoriaException(result.ToString());

            var categorias = await _categoriaRepository.GetAllCategorias();

            if (categorias.Any(p => p.Nome == categoriaDTO.Nome))
            {
                throw new CategoriaException("Já existe uma categoria com este nome");
            }

            var categoria = new Categoria
            {
                Nome = categoriaDTO.Nome,
                Descricao = categoriaDTO.Descricao,
            };
            await _categoriaRepository.AddCategoria(categoria);

            var categoriaDto = new CategoriaDTO
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
            };
            return categoriaDto;
        }
        public async Task<CategoriaDTO> UpdateCategoria(CategoriaDTO categoriaDTO, int id)
        {

            ValidationResult result = await _validator.ValidateAsync(categoriaDTO);

            if (!result.IsValid)
                throw new CategoriaException(result.ToString());

            var categorias = await _categoriaRepository.GetAllCategorias();

            if (categorias.Any(p => p.Nome == categoriaDTO.Nome))
            {
                throw new CategoriaException("Já existe uma categoria com este nome");
            }

            var categoriaExistente = await ObterCategoriaExistente(id);

            categoriaExistente.Nome = categoriaDTO.Nome;
            categoriaExistente.Descricao = categoriaDTO.Descricao;

            await _categoriaRepository.UpdateCategoria(categoriaExistente);

            var categoriaAtualizada = new CategoriaDTO
            {
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao,
            };
            return categoriaAtualizada;

        }
        public async Task DeleteCategoria(int id)
        {
            var categoria = await ObterCategoriaExistente(id);
            await _categoriaRepository.RemoveCategoria(categoria.Id);
        }
    }
}
    
