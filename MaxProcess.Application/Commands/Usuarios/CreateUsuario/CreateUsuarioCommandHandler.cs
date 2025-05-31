using MaxProcess.Application.Interfaces;
using MaxProcess.Domain.Entities;
using MaxProcess.Domain.Repositories;
using MaxProcess.Domain.UnitOfWork;
using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.CreateUsuario;

public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsuarioRepository _usuarioRepository;

    public CreateUsuarioCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUsuarioRepository usuarioRepository)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var senhaHash = _passwordHasher.Hash(request.Senha);

        var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(request.Email, cancellationToken);

        if (usuarioExistente != null)
            throw new InvalidOperationException("Já existe um usuário com este email.");

        var usuario = new Usuario(request.Login, request.Email, senhaHash);

        await _usuarioRepository.AdicionarAsync(usuario, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return usuario.Id;
    }
}
