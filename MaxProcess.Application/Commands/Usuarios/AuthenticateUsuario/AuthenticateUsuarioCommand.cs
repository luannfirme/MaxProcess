using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.AuthenticateUsuario;

public sealed record AuthenticateUsuarioCommand(string Login, string Senha) : IRequest<string?>;