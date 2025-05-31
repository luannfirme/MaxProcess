using MaxProcess.Domain.Entities;

namespace MaxProcess.Application.Shared.Security;

public interface ITokenFactory
{
    string GenerateToken(Usuario usuario);
}
