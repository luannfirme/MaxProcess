using System;
using FluentValidation;

namespace MaxProcess.Application.Commands.Usuarios.AuthenticateUsuario;

public class AuthenticateUsuarioCommandValidator : AbstractValidator<AuthenticateUsuarioCommand>
{
    public AuthenticateUsuarioCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login é obrigatório.")
            .MinimumLength(3).WithMessage("Login deve ter ao menos 3 caracteres.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter ao menos 8 caracteres.");
    }
}
