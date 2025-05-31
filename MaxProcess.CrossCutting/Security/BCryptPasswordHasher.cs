
using MaxProcess.Application.Interfaces;

namespace MaxProcess.CrossCutting.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password não pode ser vazio.", nameof(password));

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        if (string.IsNullOrEmpty(hash))
            return false;

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            return false;
        }
    }
}
