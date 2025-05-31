

using MaxProcess.Application.DTOs;
using MaxProcess.Domain.Repositories;
using MediatR;

namespace MaxProcess.Application.Queries.Usuario.GetUsuarioById;

public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto?>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public GetUsuarioByIdQueryHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id, cancellationToken);

        if (usuario is null)
            return null;

        var dto = new UsuarioDto
        {
            Id = usuario.Id,
            Login = usuario.Login,
            Email = usuario.Email,
        };

        return dto;
    }
}
