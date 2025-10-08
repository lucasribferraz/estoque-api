using estoque_api.Data;
using estoque_api.Models;
using estoque_api.Models.DTO;
using estoque_api.Services.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estoque_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriasService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriasService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllCategorias()
        {
            var categoria = await _categoriasService.GetAllCategorias();
            return Ok(categoria);
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<CategoriaDTO>> GetByIdCategoria(int id)
         {
            var categoria = await _categoriasService.GetByIdCategoria(id);
            return Ok(categoria);
         }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> AddCategoria(CategoriaDTO categoriaDTO)
        {
            var categoria = await _categoriasService.AddCategoria(categoriaDTO);
            return new ObjectResult(categoria) { StatusCode = 200 };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDTO>> UpdateCategoria(CategoriaDTO categoriaDTO, int id)
        {
            var categoriaAtualizada = await _categoriasService.UpdateCategoria(categoriaDTO, id);
            return Ok(categoriaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> DeleteCategoria(int id)
        {
            await _categoriasService.DeleteCategoria(id);
            return NoContent();
        }
    }
    
}
