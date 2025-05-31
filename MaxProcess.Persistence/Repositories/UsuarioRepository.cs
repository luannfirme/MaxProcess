using MaxProcess.Domain.Entities;
using MaxProcess.Domain.Repositories;
using MaxProcess.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MaxProcess.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        await _context.Usuarios.AddAsync(usuario, cancellationToken);
    }

    public Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        _context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }
    }
}