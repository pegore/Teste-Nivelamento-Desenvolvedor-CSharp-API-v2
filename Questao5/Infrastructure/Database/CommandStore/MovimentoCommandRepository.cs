using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Commands;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoCommandRepository : IMovimentoCommandRepository
    {
        private readonly DatabaseConfig _config;

        public MovimentoCommandRepository(DatabaseConfig config)
        {
            _config = config;
        }

        public async Task<Movimento> AddMovimentoAsync(Movimento movimento)
        {
            using var connection = new SqliteConnection(_config.Name);
            var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                    VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

            await connection.ExecuteAsync(sql, movimento);

            return movimento;
        }

        public async Task UpdateMovimentoAsync(Movimento movimento)
        {
            using (var connection = new SqliteConnection(_config.Name))
            {
                var sql = @"UPDATE movimento SET idcontacorrente = @IdContaCorrente, 
                                               datamovimento = @DataMovimento, 
                                               tipomovimento = @TipoMovimento, 
                                               valor = @Valor 
                            WHERE idmovimento = @IdMovimento";
                await connection.ExecuteAsync(sql, movimento);
            }
        }
    }
}
