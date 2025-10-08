using estoque_api.Exceptions;
using estoque_api.Models;
using estoque_api.Models.DTO;
using estoque_api.Services.Application.Interfaces;
using estoque_api.Services.Repositories.EF_Core;
using estoque_api.Services.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace estoque_api.Services.Application
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        private IValidator<ProdutoDTO> _validator;
        public ProdutoService(IValidator<ProdutoDTO> validator, IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _validator = validator;
        }

        private async Task<Produto> ObterProdutoExistente(int id)
        {
            var produto = await _produtoRepository.GetByIdProduto(id);
            if (produto == null) 
            {
                throw new ProdutoException("Produto não encontrado");
            }
            return produto;
        }
        public async Task<IEnumerable<ProdutoDTO>> GetAllProdutos()
        {
            var produtos = await _produtoRepository.GetAllProdutos();
            var produtosDTO = produtos.Select(p => new ProdutoDTO
            {
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                QuantidadeEstoque = p.QuantidadeEstoque,
                IdCategoria = p.CategoriaId,
                NomeCategoria = p.Categoria.Nome,
            }).ToList();
            return produtosDTO;
        }
        public async Task<ProdutoDTO> GetByIdProduto(int id)
        {
            var produto = await ObterProdutoExistente(id);
            var produtoDTO = new ProdutoDTO
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                IdCategoria = produto.CategoriaId,
                NomeCategoria = produto.Categoria.Nome,
            };
            return produtoDTO;
        }
        public async Task<ProdutoDTO> AddProduto(ProdutoDTO produtoDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(produtoDTO);

            if (!result.IsValid)
                throw new ProdutoException(result.ToString());

            var categoriaExistente = await _categoriaRepository.GetById(produtoDTO.IdCategoria);
            if (categoriaExistente == null)
            {
                throw new ProdutoException("Categoria não encontrada.");
            }

            var produtos = await _produtoRepository.GetAllProdutos();

            if (produtos.Any(p => p.Nome == produtoDTO.Nome)){
                throw new ProdutoException("Já existe um produto com este nome");
            }

            var produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                Preco = produtoDTO.Preco,
                QuantidadeEstoque = produtoDTO.QuantidadeEstoque,
                CategoriaId = produtoDTO.IdCategoria,
            };
            await _produtoRepository.AddProduto(produto);
            var produtoDto = new ProdutoDTO
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                IdCategoria = produto.CategoriaId,
                NomeCategoria = categoriaExistente.Nome,
            };
            return produtoDto;
        }
        public async Task<ProdutoDTO> UpdateProduto(ProdutoDTO produtoDTO, int id)
        {

            ValidationResult result = await _validator.ValidateAsync(produtoDTO);

            if (!result.IsValid)
                throw new ProdutoException(result.ToString());

            var produtoExistente = await ObterProdutoExistente(id);
            var categoriaExistente = await _categoriaRepository.GetById(produtoDTO.IdCategoria);
            if (categoriaExistente == null)
            {
                throw new ProdutoException("Categoria não encontrada");
            }

            var produtos = await _produtoRepository.GetAllProdutos();

            if (produtos.Any(p => p.Nome == produtoDTO.Nome))
            {
                throw new ProdutoException("Já existe um produto com este nome");
            }

            produtoExistente.Nome = produtoDTO.Nome;
            produtoExistente.Descricao = produtoDTO.Descricao;
            produtoExistente.Preco = produtoDTO.Preco;
            produtoExistente.QuantidadeEstoque = produtoDTO.QuantidadeEstoque;
            produtoExistente.CategoriaId = produtoDTO.IdCategoria;

            await _produtoRepository.UpdateProduto(produtoExistente);

            var produtoAtualizadoDTO = new ProdutoDTO
            {
                Nome = produtoExistente.Nome,
                Descricao = produtoExistente.Descricao,
                Preco = produtoExistente.Preco,
                QuantidadeEstoque = produtoExistente.QuantidadeEstoque,
                IdCategoria = produtoExistente.CategoriaId,
                NomeCategoria = categoriaExistente.Nome,
            };
            return produtoAtualizadoDTO;
        }
        public async Task DeleteProduto(int id)
        {
            var produto = await ObterProdutoExistente(id);
            await _produtoRepository.DeleteProduto(id);
        }
    }
}
