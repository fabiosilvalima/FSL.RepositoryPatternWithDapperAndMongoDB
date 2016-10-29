using System.Collections.Generic;
using System.Threading.Tasks;
using FSL.RepositoryPatternWithDapperAndMongoDB.Models;
using Dapper;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Repository
{
    /// <summary>
    /// Implementacao usando Dapper para SQL Server
    /// </summary>
    public class DapperTaxaRepository : SqlServerRepository, ITaxaRepository
    {
        public async Task<IEnumerable<Taxa>> ListarTaxa(int codigo)
        {
            var command = @"SELECT  Valor, Fator, Codigo 
                            FROM    Taxas 
                            WHERE   Codigo = @codigo";

            return await Database.QueryAsync<Taxa>(command, new { codigo = codigo });
        }
    }
}