using estoque_api.Data;
using estoque_api.Models;
using estoque_api.Models.DTO;
using estoque_api.Services.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace estoque_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtosService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtosService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllProdutos()
        {
            var produtos = await _produtosService.GetAllProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetByIdProduto(int id)
        {
            var produto = await _produtosService.GetByIdProduto(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> AddProduto(ProdutoDTO produtoDTO)
        {
            var produto = await _produtosService.AddProduto(produtoDTO);
            return new ObjectResult(produto) { StatusCode = 200 };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDTO>> UpdateProduto(ProdutoDTO produtoDTO, int id)
        {
            var produtoAtualizado = await _produtosService.UpdateProduto(produtoDTO, id);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteProduto(int id)
        {
            await _produtosService.DeleteProduto(id);
            return NoContent();
        }
    }
}
