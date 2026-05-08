using Microsoft.AspNetCore.Mvc;
using Models;
using Queries.Interfaces;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace SoundBitesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoController : ControllerBase
    {
        private readonly PlatoRepository _repo;

        public PlatoController(PlatoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("listar platos")]
        public async Task<ActionResult<IEnumerable<Plato>>> ListarPlatos()
        {
            var platos = await _repo.ListarPlatos();
            return Ok(platos); //200
        }

        [HttpPost("guardar plato")]
        public async Task<ActionResult<Plato>> GuardarPlato(Plato plato)
        {
            var creado = await _repo.GuardarPlato(plato);
            return StatusCode(StatusCodes.Status201Created, creado);
        }
        [HttpPut("actualizar plato/{id}")]
        public async Task<ActionResult> ActualizarPlato(int id, Plato plato)
        {
            plato.IdPlato = id;
            var filas = await _repo.ActualizarPlato(plato);

            if (filas == 0)
            {
                return NotFound();
            }

            return Ok(plato);
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarPlato(int id)
        {
            var filas = await _repo.EliminarPlato(id);
            if (filas == 0)
                return NotFound();

            return NoContent();
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Plato>> BuscarPorId(int id)
        {
            var plato = await _repo.BuscarPorId(id);
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }
    }
}