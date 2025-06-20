using System;

namespace MaxProcess.Application.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
