using FluentValidation;
using IFSPStore.Domain.Entities;

namespace IFSPStore.Service.Validators 
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Nome do produto é obrigatório.")
                .MaximumLength(100).WithMessage("Nome do produto não pode ultrapassar 100 caracteres.");
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
            RuleFor(p => p.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade do produto não pode ser negativa.");
            RuleFor(p => p.PurchaseDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de compra não pode ser no futuro.");
            RuleFor(p => p.SalesUnit)
                .MaximumLength(10).WithMessage("A unidade de venda não pode ultrapassar 10 caracteres.");
            RuleFor(p => p.Category)
                .NotNull().WithMessage("A categoria do produto é obrigatória.");
        }
    }
}
