using MaxProcess.Application.Commands.Usuarios.AuthenticateUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateUsuarioCommand command)
        {
            var token = await _mediator.Send(command);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { Message = "Email ou senha inválidos." });

            return Ok(new { Token = token });
        }
    }
}