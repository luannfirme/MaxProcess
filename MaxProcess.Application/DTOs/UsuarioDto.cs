using System;

namespace MaxProcess.Application.DTOs;

public sealed class UsuarioDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = default!;
    public string Email { get; set; } = default!;
}
