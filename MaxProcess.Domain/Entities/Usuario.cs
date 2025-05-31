namespace MaxProcess.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Login { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string SenhaHash { get; private set; } = default!;

    protected Usuario()
    {
    }
    public Usuario(string login, string email, string senhaHash)
    {
        Id = Guid.NewGuid();

        Login = login ?? throw new ArgumentNullException(nameof(login));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        SenhaHash = senhaHash ?? throw new ArgumentNullException(nameof(senhaHash));
    }

    public void AtualizarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("Hash inválida.", nameof(novaSenhaHash));

        SenhaHash = novaSenhaHash;
    }
}
