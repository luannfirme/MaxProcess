using JwtAuthApi.Models;
using JwtAuthApi.Repositories;
using JwtAuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(JwtService jwtService, IUsuarioRepository usuarioRepository)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] LoginRequest request)
        {
            var user = _usuarioRepository.GetByLogin(request.Username);
            if (user == null || user.Senha != request.Password)
                return Unauthorized("Usuário ou senha inválidos");

            var token = _jwtService.GenerateToken(user.Login);
            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
