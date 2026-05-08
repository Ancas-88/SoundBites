using Microsoft.AspNetCore.Mvc;
using Models;
using Queries.Interfaces;
using Repositories.Implementations;
using Repositories.Interfaces;


namespace SoundBitesApi.Controllers
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

        [HttpGet("listar categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarCategoria()
        {
            var categorias = await _repo.ListarCategorias();
            return Ok(categorias);
        }

        [HttpPost("guardar categoria")]
        public async Task<ActionResult<Categoria>> GuardarCategoria(Categoria categoria)
        {
            await _repo.GuardarCategoria(categoria);
            return StatusCode(StatusCodes.Status201Created, categoria);
        }

        [HttpPut("actualizar categoria/{id}")]
        public async Task<ActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            categoria.IdCategoria = id;
            var filas = await _repo.ActualizarCategoria(categoria);

            if (filas == 0)
                return NotFound();

            return Ok(categoria);
        }

        [HttpDelete("eliminar categoria/{id}")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            var filas = await _repo.EliminarCategoria(id);

            if (filas == 0)
                return NotFound();

            return NoContent();
        }

        [HttpGet("buscar por id/{id}")]
        public async Task<ActionResult<Categoria>> BuscarPorId(int id)
        {
            var categoria = await _repo.BuscarPorId(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }
    }
}