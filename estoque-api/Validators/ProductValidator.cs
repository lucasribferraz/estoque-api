using estoque_api.Models.DTO;
using FluentValidation;

namespace estoque_api.Validators
{
    public class ProductValidator : AbstractValidator<ProdutoDTO>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().Length(1, 50).WithMessage("O nome do produto deve ter entre 1 e 50 caracteres");
            RuleFor(x => x.Descricao).NotEmpty().Length(1, 100).WithMessage("A descrição do produto deve ter entre 1 e 100 caracteres");
            RuleFor(x => x.Preco).GreaterThan(0).WithMessage("O valor do produto deve ser maior que zero");
            RuleFor(x => x.QuantidadeEstoque).GreaterThanOrEqualTo(0).WithMessage("A quantidade no estoque deve ser maior ou igual a zero");
            RuleFor(x => x.IdCategoria).GreaterThan(0).WithMessage("O Id da categoria deve ser maior que zero");
            RuleFor(x => x.NomeCategoria).NotEmpty().Length(1, 50).WithMessage("O nome da categoria deve ter entre 1 e 50 caracteres");
        }
    }
}
