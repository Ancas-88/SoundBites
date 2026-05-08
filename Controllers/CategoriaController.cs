using Microsoft.AspNetCore.Mvc;
using SoundBitesAPI.Models;
using SoundBitesAPI.Repositories;

namespace SoundBitesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _repo;

        public CategoriaController(CategoriaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategorias()
        {
            var categorias = await _repo.ListarCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> BuscarPorId(int id)
        {
            var categoria = await _repo.BuscarPorId(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> GuardarCategoria(Categoria categoria)
        {
            await _repo.GuardarCategoria(categoria);
            return StatusCode(StatusCodes.Status201Created, categoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            categoria.IdCategoria = id;
            var filas = await _repo.ActualizarCategoria(categoria);
            if (filas == 0)
                return NotFound();

            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            var filas = await _repo.EliminarCategoria(id);
            if (filas == 0)
                return NotFound();

            return NoContent();
        }
    }
}
