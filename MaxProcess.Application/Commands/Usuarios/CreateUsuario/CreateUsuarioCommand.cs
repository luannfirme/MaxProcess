using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.CreateUsuario;

public sealed record CreateUsuarioCommand(string Login, string Email, string Senha) : IRequest<Guid>;