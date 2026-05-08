using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repositories.Implementations
{
    public class UsuarioRepository
    {
        private readonly IDbConnection _db;

        public UsuarioRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            var sql = "SELECT * FROM Usuarios";
            return await _db.QueryAsync<Usuario>(sql);
        }

        public async Task GuardarUsuario(Usuario usuario)
        {
            var sql = @"INSERT INTO Usuarios (Nombre, Email)
                         VALUES (@Nombre, @Email);
                         SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, usuario);
            usuario.IdUsuario = id;
        }

        public async Task<int> ActualizarUsuario(Usuario usuario)
        {
            var sql = "UPDATE Usuarios SET Nombre = @Nombre, Email = @Email WHERE IdUsuario = @IdUsuario";
            return await _db.ExecuteAsync(sql, usuario);
        }

        public async Task<int> EliminarUsuario(int id)
        {
            var sql = "DELETE FROM Usuarios WHERE IdUsuario = @Id";
            return await _db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM Usuarios WHERE IdUsuario = @Id";
            return await _db.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }
    }
}
