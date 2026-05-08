using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Implementations;

namespace SoundBitesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase
    {
        private readonly FavoritoRepository _repository;

        public FavoritoController(FavoritoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("listar-favoritos")]
        public async Task<ActionResult<IEnumerable<Favorito>>> ListarFavoritos()
        {
            var favoritos = await _repository.GetAll();

            return Ok(favoritos);
        }
        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Favorito>> BuscarPorId(int id)
        {
            var favorito = await _repository.GetById(id);
            if (favorito == null)
            {
                return NotFound();
            }
            return Ok(favorito);
        }
        [HttpPost("guardar-favorito")]
        public async Task<ActionResult<Favorito>> GuardarFavorito(Favorito favorito)
        {
            var nuevoFavorito = await _repository.Add(favorito);
            return StatusCode(StatusCodes.Status201Created, nuevoFavorito);
        }
        [HttpDelete("eliminar-favorito/{id}")]
        public async Task<ActionResult> EliminarFavorito(int id)
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