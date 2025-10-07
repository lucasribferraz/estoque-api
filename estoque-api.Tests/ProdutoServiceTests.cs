using estoque_api.Exceptions;
using estoque_api.Models;
using estoque_api.Models.DTO;
using estoque_api.Services.Application;
using estoque_api.Validators;

namespace estoque_api.Tests
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task GetById_DeveRetornarProdutoDTO_QuandoIdInseridoForCorreto()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática"
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValidoCat = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Notebook",
                Descricao = "Notebook Gamer",
                Preco = 4000,
                QuantidadeEstoque = 100,
                IdCategoria = idValidoCat,
                NomeCategoria = "Informática",
            };

            await produtoService.AddProduto(produtoDTO);

            var produtos = await produtoFake.GetAllProdutos();
            var idValidoProd = produtos.Last().Id;

            //Act
            var resultadoId = await produtoService.GetByIdProduto(idValidoProd);

            //Assert
            Assert.Equal(produtoDTO.Nome, resultadoId.Nome);
            Assert.Equal(produtoDTO.Preco, resultadoId.Preco);
            Assert.Equal(produtoDTO.Descricao, resultadoId.Descricao);
            Assert.Equal(produtoDTO.QuantidadeEstoque, resultadoId.QuantidadeEstoque);
            Assert.Equal(produtoDTO.IdCategoria, resultadoId.IdCategoria);
            Assert.Equal(produtoDTO.NomeCategoria, resultadoId.NomeCategoria);
        }

        [Fact]
        public async Task GetById_DeveLancarExcecao_QuandoIdForInvalido()
        {
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            int idInvalido = 9999;

            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.GetByIdProduto(idInvalido);
            });
        }

        [Fact]
        public async Task AddProduto_DeveRetornarProdutoDTO_QuandoCategoriaExiste()
        {

            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Notebook",
                Descricao = "Notebook Gamer",
                Preco = 4000,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            //Act
            var produtoRetornado = await produtoService.AddProduto(produtoDTO);

            //Assert
            Assert.Equal(produtoDTO.Nome, produtoRetornado.Nome);
            Assert.Equal(produtoDTO.Descricao, produtoRetornado.Descricao);
            Assert.Equal(produtoDTO.Preco, produtoRetornado.Preco);
            Assert.Equal(produtoDTO.QuantidadeEstoque, produtoRetornado.QuantidadeEstoque);
            Assert.Equal(produtoDTO.IdCategoria, produtoRetornado.IdCategoria);
            Assert.Equal(produtoDTO.NomeCategoria, produtoRetornado.NomeCategoria);
        }

        [Fact]
        public async Task AddProduto_DeveLancarExcecao_QuandoCategoriaNaoExiste()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Notebook",
                Descricao = "Notebook Gamer",
                Preco = 4000,
                QuantidadeEstoque = 100,
                IdCategoria = 999,
                NomeCategoria = "Informática",
            };

            //Assert (Act fica dentro do Assert neste caso)
            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.AddProduto(produtoDTO);
            });
        }

        [Fact]
        public async Task AddProduto_DeveLancarExcecao_QuandoProdutoForIgual()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);

            var categoriaDTO = new CategoriaDTO()
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO()
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            var produtoCriado = await produtoService.AddProduto(produtoDTO);

            //Act e Assert
            var produtoDuplicado = new ProdutoDTO()
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.AddProduto(produtoDuplicado);
            });
        }

        [Fact]
        public async Task UpdateProduto_DeveRetornarProdutoAtualizado_QuandoIdEProdutoForemValidos()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            await produtoService.AddProduto(produtoDTO);

            var produtos = await produtoFake.GetAllProdutos();
            var idValidoProd = produtos.Last().Id;

            var produtoAtualizadoDTO = new ProdutoDTO
            {
                Nome = "Monitor LG",
                Descricao = "Monitor LG Ultrawide",
                Preco = 650,
                QuantidadeEstoque = 80,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            //Act
            var resultado = await produtoService.UpdateProduto(produtoAtualizadoDTO, idValidoProd);

            //Assert
            Assert.Equal(resultado.Nome, produtoAtualizadoDTO.Nome);
            Assert.Equal(resultado.Descricao, produtoAtualizadoDTO.Descricao);
            Assert.Equal(resultado.Preco, produtoAtualizadoDTO.Preco);
            Assert.Equal(resultado.QuantidadeEstoque, produtoAtualizadoDTO.QuantidadeEstoque);
            Assert.Equal(resultado.IdCategoria, produtoAtualizadoDTO.IdCategoria);
            Assert.Equal(resultado.NomeCategoria, produtoAtualizadoDTO.NomeCategoria);
        }

        [Fact]
        public async Task UpdateProduto_DeveLancarExcecao_QuandoCategoriaForInvalida()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidation = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidation, produtoFake, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            await produtoService.AddProduto(produtoDTO);

            var produtos = await produtoFake.GetAllProdutos();
            var idValidoProd = produtos.Last().Id;

            var produtoDTO2 = new ProdutoDTO
            {
                Nome = "Monitor LG",
                Descricao = "Monitor LG Ultrawide",
                Preco = 650,
                QuantidadeEstoque = 80,
                IdCategoria = 999,
                NomeCategoria = "",
            };

            //Act e Assert
            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.UpdateProduto(produtoDTO2, idValidoProd);
            });
        }

        [Fact]
        public async Task UpdateProduto_DeveLancarExcecao_QuandoIdForInvalido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Monitor LG",
                Descricao = "Monitor LG Ultrawide",
                Preco = 650,
                QuantidadeEstoque = 80,
                IdCategoria = 1,
                NomeCategoria = "Informática"
            };

            int idInvalidoProd = 9999;

            //Act & Assert
            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.UpdateProduto(produtoDTO, idInvalidoProd);
            });
        }

        [Fact]
        public async Task UpdateProduto_DeveLancarExcecao_QuandoNomeProdutoForIgualAProdutoExistente()
        {
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            await produtoService.AddProduto(produtoDTO);

            var produtos = await produtoFake.GetAllProdutos();
            var idValidoProd = produtos.Last().Id;

            var produtoDTO2 = new ProdutoDTO
            {
                Nome = "Monitor LG",
                Descricao = "Monitor LG Ultrawide",
                Preco = 650,
                QuantidadeEstoque = 80,
                IdCategoria = idValido,
                NomeCategoria = "Informática"
            };

            await produtoService.AddProduto(produtoDTO2);

            var produtos2 = await produtoFake.GetAllProdutos();
            var idValidoProd2 = produtos2.Last().Id;

            var produtoAtualizado = new ProdutoDTO
            {
                Nome = "Monitor",
                Descricao = "Monitor LG Ultrawide",
                Preco = 650,
                QuantidadeEstoque = 80,
                IdCategoria = idValido,
                NomeCategoria = "Informática"
            };

            //Act e Assert

            var ex = await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.UpdateProduto(produtoAtualizado, idValidoProd2);
            });
        }

        [Fact]
        public async Task DeleteProduto_DeveRemoverProduto_QuandoIdForValido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            var categoriaDTO = new CategoriaDTO
            {
                Nome = "Informática",
                Descricao = "Categoria onde armazena todos os produtos de informática",
            };

            await categoriaService.AddCategoria(categoriaDTO);

            var categorias = await categoriaFake.GetAllCategorias();
            var idValido = categorias.Last().Id;

            var produtoDTO = new ProdutoDTO
            {
                Nome = "Monitor",
                Descricao = "Monitor Samsung",
                Preco = 500,
                QuantidadeEstoque = 100,
                IdCategoria = idValido,
                NomeCategoria = "Informática",
            };

            await produtoService.AddProduto(produtoDTO);

            var produtos = await produtoFake.GetAllProdutos();
            var idValidoProd = produtos.Last().Id;

            //Act
            await produtoService.DeleteProduto(idValidoProd);

            //Assert
            var produtosAposRemocao = await produtoFake.GetAllProdutos();
            Assert.DoesNotContain(produtosAposRemocao, p => p.Id == idValidoProd);
        }

        [Fact]
        public async Task DeleteProduto_DeveLancarExcecao_QuandoIdForInvalido()
        {
            //Arrange
            var categoriaFake = new CategoriaRepositoryFake();
            var produtoFake = new ProdutoRepositoryFake(categoriaFake);
            var produtoValidator = new ProductValidator();
            var categoriaValidator = new CategoriaValidator();
            var categoriaService = new CategoriaService(categoriaValidator, categoriaFake);
            var produtoService = new ProdutoService(produtoValidator, produtoFake, categoriaFake);

            int IdInvalido = 999;

            //Act e Assert
            await Assert.ThrowsAsync<ProdutoException>(async () =>
            {
                await produtoService.DeleteProduto(IdInvalido);
            });
        }
    }
}