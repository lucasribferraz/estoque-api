using estoque_api.Models.DTO;
using FluentValidation;

namespace estoque_api.Validators
{
    public class CategoriaValidator : AbstractValidator<CategoriaDTO>
    {
        public CategoriaValidator() 
        {
            RuleFor(x => x.Nome).NotEmpty().Length(1, 50).WithMessage("O nome da categoria deve ter entre 1 e 50 caracteres");
            RuleFor(x => x.Descricao).NotEmpty().Length(1, 100).WithMessage("A descrição da categoria deve ter entre 1 e 100 caracteres");
        }
    }
}
