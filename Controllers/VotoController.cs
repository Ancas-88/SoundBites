using Microsoft.AspNetCore.Mvc;
using Models;
using Queries.Interfaces;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace SoundBitesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly VotoRepository _repo;

        public VotoController(VotoRepository repo)
        {
            _repo = repo;
        }   
        [HttpGet("listar votos")]
        public async Task<ActionResult<IEnumerable<Voto>>> ListarVotos()
        {
            var votos = await _repo.ListarVotos();
            return Ok(votos); //200
        }

        [HttpPost("guardar voto")]
        public async Task<ActionResult<Voto>> GuardarVoto(Voto voto)
        {
            await _repo.GuardarVoto(voto);
            return StatusCode(StatusCodes.Status201Created, voto);
        }
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarVoto(int id)
        {
            var filas = await _repo.EliminarVoto(id);

            if (filas == 0)
            {
                return NotFound();
            }
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Voto>> BuscarPorId(int id)
        {
            var voto = await _repo.BuscarPorId(id);
            if (voto == null)
            {
                return NotFound();
            }
            return Ok(voto);
        }
    }
}