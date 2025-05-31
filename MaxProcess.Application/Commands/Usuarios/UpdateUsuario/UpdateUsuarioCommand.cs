using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.UpdateUsuario;

public sealed record UpdateUsuarioCommand(Guid Id, string Login, string Email, string? Senha) : IRequest<bool>;