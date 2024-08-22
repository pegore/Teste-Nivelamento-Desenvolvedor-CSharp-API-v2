using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Queries;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class ContaCorrenteQueryRepository : IContaCorrenteQueryRepository
    {
        private readonly DatabaseConfig _config;

        public ContaCorrenteQueryRepository(DatabaseConfig config)
        {
            _config = config;
        }

        public async Task<ContaCorrente> GetByNumeroContaCorrenteIdAsync(int numeroContaCorrente)
        {
            using var connection = new SqliteConnection(_config.Name);
            var query = await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
                "SELECT * FROM contacorrente WHERE numero = @Numero", new { Numero = numeroContaCorrente });
            return query;
       }
    }
}
