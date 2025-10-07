using estoque_api.Models;
using estoque_api.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estoque_api.Tests
{
    public class CategoriaRepositoryFake: ICategoriaRepository
    {
        private readonly List<Categoria> _categorias = new();
        private int _nextId = 1;
        public Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            return Task.FromResult<IEnumerable<Categoria>>(_categorias);
        }
        public Task<Categoria?> GetById(int id)
        {
            return Task.FromResult<Categoria?>(_categorias.FirstOrDefault(c => c.Id == id));
        }
        public Task<Categoria> AddCategoria(Categoria categoria)
        {
            _categorias.Add(categoria);
            categoria.Id = _nextId++;
            return Task.FromResult<Categoria>(categoria);
        }
        public Task<Categoria?> UpdateCategoria(Categoria categoria)
        {
            var categoriaExistente = _categorias.FirstOrDefault(c => c.Id == categoria.Id);
            if (categoriaExistente == null) 
            { 
            return Task.FromResult<Categoria?>(null);
            }

            categoriaExistente.Nome = categoria.Nome;
            categoriaExistente.Descricao = categoria.Descricao;

            return Task.FromResult<Categoria?>(categoriaExistente);
        }
        public Task<Categoria?> RemoveCategoria(int id)
        {
            var categoriaExistente = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoriaExistente == null)
            {
                return Task.FromResult<Categoria?>(null);
            }

            _categorias.Remove(categoriaExistente);
            return Task.FromResult<Categoria?>(categoriaExistente);
        }
        
    }
}