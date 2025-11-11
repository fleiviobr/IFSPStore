using FluentValidation;
using IFSPStore.Domain.Entities;

namespace IFSPStore.Service.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Nome da categoria é obrigatório.")
                .MaximumLength(50).WithMessage("Nome da categoria não pode ultrapassar 50 caracteres.");
        }
    }
}
