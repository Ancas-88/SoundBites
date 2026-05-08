using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class FavoritoRepository
    {
        private readonly IDbConnection _db;

        public FavoritoRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Favorito>> GetAll()
        {
            var sql = "SELECT * FROM Favoritos";
            return await _db.QueryAsync<Favorito>(sql);
        }

        public async Task<Favorito?> GetById(int id)
        {
            var sql = "SELECT * FROM Favoritos WHERE IdFavorito = @Id";
            return await _db.QueryFirstOrDefaultAsync<Favorito>(sql, new { Id = id });
        }

        public async Task<Favorito> Add(Favorito favorito)
        {
            var sql = @"INSERT INTO Favoritos (IdUsuario, IdPlato)
                         VALUES (@IdUsuario, @IdPlato);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, favorito);
            favorito.IdFavorito = id;
            return favorito;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Favoritos WHERE IdFavorito = @Id";
            var rows = await _db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
