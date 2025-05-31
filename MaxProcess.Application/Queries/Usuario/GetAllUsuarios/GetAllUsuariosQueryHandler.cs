using MaxProcess.Application.DTOs;
using MaxProcess.Domain.Repositories;
using MediatR;

namespace MaxProcess.Application.Queries.Usuario.GetAllUsuarios;

public class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, IEnumerable<UsuarioDto>>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public GetAllUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync(cancellationToken);

        var dtos = usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Login = u.Login,
            Email = u.Email,
        });

        return dtos;
    }
}
