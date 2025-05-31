using MaxProcess.Domain.Repositories;

namespace MaxProcess.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task Commit(CancellationToken cancellationToken);
}
