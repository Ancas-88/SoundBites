using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class GeneroRepository
    {
        private readonly IDbConnection _db;

        public GeneroRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genero>> GetAll()
        {
            var sql = "SELECT * FROM Generos";
            return await _db.QueryAsync<Genero>(sql);
        }

        public async Task<Genero?> GetById(int id)
        {
            var sql = "SELECT * FROM Generos WHERE IdGenero = @Id";
            return await _db.QueryFirstOrDefaultAsync<Genero>(sql, new { Id = id });
        }

        public async Task<Genero> Add(Genero genero)
        {
            var sql = @"INSERT INTO Generos (Nombre)
                         VALUES (@Nombre);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, genero);
            genero.IdGenero = id;
            return genero;
        }

        public async Task<bool> Update(int id, Genero genero)
        {
            var sql = "UPDATE Generos SET Nombre = @Nombre WHERE IdGenero = @Id";
            var rows = await _db.ExecuteAsync(sql, new { genero.Nombre, Id = id });
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = "DELETE FROM Generos WHERE IdGenero = @Id";
            var rows = await _db.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
