using MaxProcess.Application.DTOs;
using MediatR;

namespace MaxProcess.Application.Queries.Usuario.GetUsuarioById;

public sealed record GetUsuarioByIdQuery(Guid Id) : IRequest<UsuarioDto?>;
