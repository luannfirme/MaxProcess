using MaxProcess.Application.DTOs;
using MediatR;

namespace MaxProcess.Application.Queries.Usuario.GetUsuarioById;

public class GetUsuarioByIdQuery : IRequest<UsuarioDto?>
{
    public Guid Id { get; init; }

    public GetUsuarioByIdQuery(Guid id)
    {
        Id = id;
    }
}
