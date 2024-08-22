using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Queries;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class MovimentoQueryRepository : IMovimentoQueryRepository
    {
        private readonly DatabaseConfig _config;

        public MovimentoQueryRepository(DatabaseConfig config)
        {
            _config = config;
        }

        public async Task<decimal> SomarCreditosAsync(string idContaCorrente)
        {
            using var connection = new SqliteConnection(_config.Name);
            var sql = @"SELECT IFNULL(SUM(valor), 0)
                    FROM movimento
                    WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'C';";

            return await connection.ExecuteScalarAsync<decimal>(sql, new { IdContaCorrente = idContaCorrente });
        }

        public async Task<decimal> SomarDebitosAsync(string idContaCorrente)
        {
            using var connection = new SqliteConnection(_config.Name);
            var sql = @"SELECT IFNULL(SUM(valor), 0)
                    FROM movimento
                    WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'D';";

            return await connection.ExecuteScalarAsync<decimal>(sql, new { IdContaCorrente = idContaCorrente });
        }
    }
}
