using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Implementations;

namespace SoundBitesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroRepository _repository;

        public GeneroController(GeneroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("listar-generos")]
        public async Task<ActionResult<IEnumerable<Genero>>> ListarGeneros()
        {
            var generos = await _repository.GetAll();

            return Ok(generos);
        }
        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Genero>> BuscarPorId(int id)
        {
            var genero = await _repository.GetById(id);

            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }
        [HttpPost("guardar-genero")]
        public async Task<ActionResult<Genero>> GuardarGenero(Genero genero)
        {
            var nuevoGenero = await _repository.Add(genero);

            return StatusCode(StatusCodes.Status201Created, nuevoGenero);
        }
        [HttpPut("actualizar-genero/{id}")]
        public async Task<ActionResult> ActualizarGenero(int id, Genero genero)
        {
            var actualizado = await _repository.Update(id, genero);

            if (!actualizado)
            {
                return NotFound();
            }

            return Ok(genero);
        }
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarGenero(int id)
        {
            var eliminado = await _repository.Delete(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
