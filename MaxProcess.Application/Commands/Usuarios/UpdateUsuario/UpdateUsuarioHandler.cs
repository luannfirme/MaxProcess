using MaxProcess.Application.Interfaces;
using MaxProcess.Domain.Repositories;
using MaxProcess.Domain.UnitOfWork;
using MediatR;

namespace MaxProcess.Application.Commands.Usuarios.UpdateUsuario;

public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsuarioRepository _usuarioRepository;

    public UpdateUsuarioHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUsuarioRepository usuarioRepository)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id, cancellationToken);
        
        if (usuario is null)
            return false;

        usuario.Login = request.Login;
        usuario.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            var novaHash = _passwordHasher.Hash(request.Senha);
            usuario.AtualizarSenha(novaHash);
        }

        await _usuarioRepository.AtualizarAsync(usuario, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return true;
    }
}
