using estoque_api.Data;
using estoque_api.Models;
using estoque_api.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace estoque_api.Services.Repositories.EF_Core
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }
        public async Task<Produto?> GetByIdProduto(int id)
        {
            return await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Produto> AddProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;

        }
        public async Task<Produto?> UpdateProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
        public async Task<Produto?> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return null;
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

     
    }
}
