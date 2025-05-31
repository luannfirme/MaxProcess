using JwtAuthApi.Models;
using JwtAuthApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var usuario = _repository.GetById(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if (_repository.ExistsByLoginOrEmail(usuario.Login, usuario.Email))
                return Conflict("Já existe um usuário com este login ou email.");

            _repository.Create(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Usuario usuario)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();

            usuario.Id = id;
            _repository.Update(usuario);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}
