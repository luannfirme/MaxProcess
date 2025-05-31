using FluentValidation;

namespace MaxProcess.Application.Commands.Usuarios.UpdateUsuario;

public class UpdateUsuarioValidator : AbstractValidator<UpdateUsuarioCommand>
{
    public UpdateUsuarioValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do usuário é obrigatório.");

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login é obrigatório.")
            .MinimumLength(3).WithMessage("Login deve ter ao menos 3 caracteres.")
            .MaximumLength(50).WithMessage("Login deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        When(x => !string.IsNullOrWhiteSpace(x.Senha), () =>
        {
            RuleFor(x => x.Senha!)
                .MinimumLength(8).WithMessage("Senha deve ter ao menos 8 caracteres.")
                .NotEmpty().WithMessage("Senha não pode ser vazia quando fornecida.");
        });
    }
}
