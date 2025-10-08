using estoque_api.Data;
using estoque_api.Models;
using estoque_api.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estoque_api.Services.Repositories.EF_Core
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria?> GetById(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<Categoria> AddCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria?> UpdateCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
        public async Task<Categoria?> RemoveCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return null;
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}

