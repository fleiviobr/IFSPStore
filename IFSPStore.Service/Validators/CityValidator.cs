using FluentValidation;
using IFSPStore.Domain.Entities;

namespace IFSPStore.Service.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome da cidade é obrigatório.");
            RuleFor(c => c.State)
                .NotEmpty().WithMessage("Estado é obrigatório.")
                .Length(2).WithMessage("Estado deve ter exatamente 2 caracteres.");
        }
    }
}
