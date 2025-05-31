using MaxProcess.Application.DTOs;
using MediatR;

namespace MaxProcess.Application.Queries.Usuario.GetAllUsuarios;

public sealed record GetAllUsuariosQuery : IRequest<IEnumerable<UsuarioDto>>;