using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class PlatoRepository
    {
        private readonly IDbConnection _db;

        public PlatoRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Plato>> ListarPlatos()
        {
            var sql = "SELECT * FROM Platos";
            return await _db.QueryAsync<Plato>(sql);
        }

        public async Task<Plato> GuardarPlato(Plato plato)
        {
            var sql = @"INSERT INTO Platos (Nombre, Descripcion, Precio, IdCategoria)
                         VALUES (@Nombre, @Descripcion, @Precio, @IdCategoria);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, plato);
            plato.IdPlato = id;
            return plato;
        }

        public async Task<Plato?> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM Platos WHERE IdPlato = @Id";
            return await _db.QueryFirstOrDefaultAsync<Plato>(sql, new { Id = id });
        }

        public async Task<int> ActualizarPlato(Plato plato)
        {
            var sql = @"UPDATE Platos SET Nombre = @Nombre, Descripcion = @Descripcion,
                         Precio = @Precio, IdCategoria = @IdCategoria
                         WHERE IdPlato = @IdPlato";
            return await _db.ExecuteAsync(sql, plato);
        }

        public async Task<int> EliminarPlato(int id)
        {
            var sql = "DELETE FROM Platos WHERE IdPlato = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
