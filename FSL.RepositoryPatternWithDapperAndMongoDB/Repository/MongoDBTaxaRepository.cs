using FSL.RepositoryPatternWithDapperAndMongoDB.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Repository
{
    /// <summary>
    /// Implementação usando MongoDB
    /// </summary>
    public class MongoDBTaxaRepository : MongoDBRepository, ITaxaRepository
    {
        public async Task<IEnumerable<Taxa>> ListarTaxa(int codigo)
        {
            // comente esse bloco quando instalar o mongodb, criar a tabela e carregar de dados.
            return await Task.Run(() =>
            {
                var taxas = new List<Taxa>();
                taxas.Add(new Taxa { Codigo = 1, Valor = 10, Fator = 2 });

                return taxas;
            });

            //codigo da implementacao de mongodb
            return await Database.GetCollection<Taxa>("Taxas")
                .Find(x => x.Codigo == codigo)
                .ToListAsync();
        }
    }
}