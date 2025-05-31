using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MaxProcess.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MaxProcess.Application.Shared.Security;

public class JwtTokenFactory : ITokenFactory
{
    private readonly JwtSettings _jwtSettings;
    private readonly byte[] _keyBytes;

    public JwtTokenFactory(IOptions<JwtSettings> options)
    {
        _jwtSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        _keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);
    }

    public string GenerateToken(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("login", usuario.Login)
            };

        var securityKey = new SymmetricSecurityKey(_keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
