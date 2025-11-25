using FluentValidation;
using IFSPStore.Domain.Entities;

namespace IFSPStore.Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100).WithMessage("Nome não pode ultrapassar 100 caracteres.");
            RuleFor(user => user.Login)
                .NotEmpty().WithMessage("Login é obrigatório.")
                .MaximumLength(50).WithMessage("Login não pode ultrapassar 50 caracteres.");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Senha é obrigatório.")
                .MinimumLength(5).WithMessage("Senha must be at least 5 caracteres long.");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email é invalido.");
    }
}
}
