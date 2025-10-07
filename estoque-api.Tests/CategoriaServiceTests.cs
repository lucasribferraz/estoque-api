using estoque_api.Exceptions;
using estoque_api.Models.DTO;
using estoque_api.Services.Application;
using estoque_api.Services.Application.Interfaces;
using estoque_api.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estoque_api.Tests
{
    public class CategoriaServiceTests
    {
        [Fact]
        public async Task GetById_DeveRetornarCategoria_QuandoIdForValido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            //Act
            var resultado = await categoriaService.GetByIdCategoria(idValidoCat);

            //Assert
            Assert.Equal(categoriaDTO.Nome, resultado.Nome);
            Assert.Equal(categoriaDTO.Descricao, resultado.Descricao);
        }

        [Fact]
        public async Task GetById_DeveLancarExcecao_QuandoIdForInvalido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            int idInvalido = 999;

            //Act e Assert
            await Assert.ThrowsAsync<CategoriaException>(async () =>
            {
                await categoriaService.GetByIdCategoria(idInvalido);
            });
        }

        [Fact]
        public async Task AddCategoria_DeveRetornarCategoriaDTO_QuandoDadosForemValidos()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            //Act
            var resultado = await categoriaService.AddCategoria(categoriaDTO);

            //Assert
            Assert.Equal(categoriaDTO.Nome, resultado.Nome);
            Assert.Equal(categoriaDTO.Descricao, resultado.Descricao);
        }

        [Fact]
        public async Task AddCategoria_DeveLancarExcecao_QuandoNomeForDuplicado()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            var categoriaDTO2 = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            //Act e Assert
            await Assert.ThrowsAsync<CategoriaException>(async () =>
            {
                await categoriaService.AddCategoria(categoriaDTO2);
            });
        }

        [Fact]
        public async Task UpdateCategoria_DeveAtualizarCategoria_QuandoIdForValido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            var categoriaAtualizada = new CategoriaDTO
            {
                Nome = "Games",
                Descricao = "Categoria onde armazena console e jogos"
            };

            // Act
            await categoriaService.UpdateCategoria(categoriaAtualizada, idValidoCat);

            // Assert: buscar novamente e comparar
            var categoriaDepois = await categoriaService.GetByIdCategoria(idValidoCat);
            Assert.Equal(categoriaAtualizada.Nome, categoriaDepois.Nome);
            Assert.Equal(categoriaAtualizada.Descricao, categoriaDepois.Descricao);
        }

        [Fact]
        public async Task UpdateCategoria_DeveLancarExcecao_QuandoIdForInvalido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            int idInvalido = 999;

            //Act e Assert
            await Assert.ThrowsAsync<CategoriaException>(async () =>
            {
                await categoriaService.UpdateCategoria(categoriaDTO, idInvalido);
            });
        }

        [Fact]
        public async Task UpdateCategoria_DeveLancarExcecao_QuandoNomeForDuplicado()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            var categoriaDTO2 = new CategoriaDTO
            {
                Nome = "Games",
                Descricao = "Categoria onde armazena console e jogos"
            };

            await categoriaService.AddCategoria(categoriaDTO2);

            var categorias2 = await categoriaFake.GetAllCategorias();
            var idValidoCat2 = categorias2.Last().Id;

            var categoriaAtualizada = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            //Act e Assert
            await Assert.ThrowsAsync<CategoriaException>(async () =>
            {
                await categoriaService.UpdateCategoria(categoriaAtualizada, idValidoCat2);
            });
        }

        [Fact]
        public async Task DeleteCategoria_DeveRemoverCategoria_QuandoIdForValido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            //Act
            await categoriaService.DeleteCategoria(idValidoCat);

            //Assert
            var categoriasAposRemocao = await categoriaFake.GetAllCategorias();
            Assert.DoesNotContain(categoriasAposRemocao, p => p.Id == idValidoCat);
        }

        [Fact]
        public async Task DeleteCategoria_DeveLancarExcecao_QuandoIdForInvalido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            int idInvalido = 999;

            //Act e Assert
            await Assert.ThrowsAsync<CategoriaException>(async () =>
            {
                await categoriaService.DeleteCategoria(idInvalido);
            });
        }
    }
}
