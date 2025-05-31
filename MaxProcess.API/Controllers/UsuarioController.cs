using MaxProcess.Application.Commands.Usuarios.CreateUsuario;
using MaxProcess.Application.Commands.Usuarios.UpdateUsuario;
using MaxProcess.Application.DTOs;
using MaxProcess.Application.Queries.Usuario.GetAllUsuarios;
using MaxProcess.Application.Queries.Usuario.GetUsuarioById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            var query =  new GetAllUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return Ok(usuarios);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UsuarioDto?>> GetById(Guid id)
        {
            var query = new GetUsuarioByIdQuery(id);
            var usuario = await _mediator.Send(query);
            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUsuarioCommand command)
        {
            var novoId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = novoId }, novoId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUsuarioCommand command)
        {
            if (id != command.Id)
                return BadRequest("O id da URL deve ser igual ao id do corpo da requisição.");

            var updated = await _mediator.Send(command);
            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}