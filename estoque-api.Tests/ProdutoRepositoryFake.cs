using estoque_api.Data;
using estoque_api.Models;
using estoque_api.Services.Repositories.EF_Core;
using estoque_api.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estoque_api.Tests
{
    public class ProdutoRepositoryFake : IProdutoRepository
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public readonly List<Produto> _produtos = new();

        private int _nextId = 1;

        public ProdutoRepositoryFake(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public Task<IEnumerable<Produto>> GetAllProdutos()
        {
            return Task.FromResult<IEnumerable<Produto>>(_produtos);
        }
        public Task<Produto?> GetByIdProduto(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                produto.Categoria = _categoriaRepository.GetById(produto.CategoriaId).Result;
            }
            return Task.FromResult(produto);
        }
        public Task<Produto> AddProduto(Produto produto)
        {
            produto.Id = _nextId++;
            _produtos.Add(produto);
            return Task.FromResult<Produto>(produto);
        }
        public Task<Produto?> UpdateProduto(Produto produto)
        {
            var produtoExistente = _produtos.FirstOrDefault(p => p.Id == produto.Id);
            if (produtoExistente == null)
            {
                return Task.FromResult<Produto?>(null);
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;
            produtoExistente.CategoriaId = produto.CategoriaId;
            produtoExistente.Categoria = produto.Categoria;

            return Task.FromResult<Produto?>(produtoExistente);
        }
        public Task<Produto?> DeleteProduto(int id)
        {
            var produtoExistente = _produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente == null)
            {
                return Task.FromResult<Produto?>(null);
            }
            
            _produtos.Remove(produtoExistente);
            return Task.FromResult<Produto?>(produtoExistente);
        }
    }
}
