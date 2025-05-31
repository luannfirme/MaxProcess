namespace MaxProcess.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Login { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Senha { get; private set; } = default!;

    protected Usuario()
    {
    }
    public Usuario(string login, string email, string senha)
    {
        Id = Guid.NewGuid();

        Login = login ?? throw new ArgumentNullException(nameof(login));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Senha = senha ?? throw new ArgumentNullException(nameof(senha));
    }

    public void AtualizarSenha(string novaSenha)
    {
        if (string.IsNullOrWhiteSpace(novaSenha))
            throw new ArgumentException("Hash inválida.", nameof(novaSenha));

        Senha = novaSenha;
    }
}
