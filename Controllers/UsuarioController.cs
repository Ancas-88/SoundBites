using Microsoft.AspNetCore.Mvc;
using Models;
using Queries.Interfaces;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace SoundBitesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController:ControllerBase
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("listar usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuario()
        {
            var usuarios = await _repo.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpPost("guardar usuario")]
        public async Task<ActionResult<Usuario>> GuardarUsuario(Usuario usuario)
        {
            await _repo.GuardarUsuario(usuario);
            return StatusCode(StatusCodes.Status201Created, usuario);
        }

        [HttpPut("actualizar usuario/{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, Usuario usuario)
        {
            usuario.IdUsuario = id;
            var filas = await _repo.ActualizarUsuario(usuario);

            if (filas == 0)
                return NotFound();

            return Ok(usuario);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var filas = await _repo.EliminarUsuario(id);

            if (filas == 0)
                return NotFound();

            return NoContent();
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(int id)
        {
            var usuario = await _repo.BuscarPorId(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
    }
}
    }
}
