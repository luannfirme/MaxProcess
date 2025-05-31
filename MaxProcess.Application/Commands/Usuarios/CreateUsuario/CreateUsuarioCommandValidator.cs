using FluentValidation;

namespace MaxProcess.Application.Commands.Usuarios.CreateUsuario;

public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
{
    public CreateUsuarioCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login é obrigatório.")
            .MinimumLength(3).WithMessage("Login deve ter ao menos 3 caracteres.")
            .MaximumLength(50).WithMessage("Login deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter ao menos 8 caracteres.");
    }
}
