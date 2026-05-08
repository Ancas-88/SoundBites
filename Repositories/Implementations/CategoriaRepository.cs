using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class CategoriaRepository
    {
        private readonly IDbConnection _db;

        public CategoriaRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Categoria>> ListarCategorias()
        {
            var sql = "SELECT * FROM Categorias";
            return await _db.QueryAsync<Categoria>(sql);
        }

        public async Task GuardarCategoria(Categoria categoria)
        {
            var sql = @"INSERT INTO Categorias (Nombre)
                         VALUES (@Nombre);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, categoria);
            categoria.IdCategoria = id;
        }

        public async Task<int> ActualizarCategoria(Categoria categoria)
        {
            var sql = "UPDATE Categorias SET Nombre = @Nombre WHERE IdCategoria = @IdCategoria";
            return await _db.ExecuteAsync(sql, categoria);
        }

        public async Task<int> EliminarCategoria(int id)
        {
            var sql = "DELETE FROM Categorias WHERE IdCategoria = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Categoria?> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM Categorias WHERE IdCategoria = @Id";
            return await _db.QueryFirstOrDefaultAsync<Categoria>(sql, new { Id = id });
        }
    }
}
