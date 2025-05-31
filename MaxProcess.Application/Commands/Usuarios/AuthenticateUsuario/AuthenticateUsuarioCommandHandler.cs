using System;
using MaxProcess.Application.Interfaces;
using MaxProcess.Application.Shared.Security;
using MaxProcess.Domain.Repositories;
using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.AuthenticateUsuario;

public class AuthenticateUsuarioCommandHandler : IRequestHandler<AuthenticateUsuarioCommand, string?>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenFactory _tokenFactory;

    public AuthenticateUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher, ITokenFactory tokenFactory)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _tokenFactory = tokenFactory;
    }

    public async Task<string?> Handle(AuthenticateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorLoginAsync(request.Login, cancellationToken);

            if (usuario is null)
                return null;

            var hashArmazenado = usuario.Senha;

            var senhaValida = _passwordHasher.Verify(request.Senha, hashArmazenado);
            if (!senhaValida)
                return null;

            var token = _tokenFactory.GenerateToken(usuario);

            return token;
    }
}
