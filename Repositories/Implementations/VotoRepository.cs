using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class VotoRepository
    {
        private readonly IDbConnection _db;

        public VotoRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Voto>> ListarVotos()
        {
            var sql = "SELECT * FROM Votos";
            return await _db.QueryAsync<Voto>(sql);
        }

        public async Task GuardarVoto(Voto voto)
        {
            var sql = @"INSERT INTO Votos (IdUsuario, IdPlato, Valor)
                         VALUES (@IdUsuario, @IdPlato, @Valor);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, voto);
            voto.IdVoto = id;
        }

        public async Task<int> EliminarVoto(int id)
        {
            var sql = "DELETE FROM Votos WHERE IdVoto = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id });
        }

        public Task SaveChangesAsync()
        {
            // no-op for Dapper/IDbConnection
            return Task.CompletedTask;
        }

        public async Task<Voto?> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM Votos WHERE IdVoto = @Id";
            return await _db.QueryFirstOrDefaultAsync<Voto>(sql, new { Id = id });
        }
    }
}
