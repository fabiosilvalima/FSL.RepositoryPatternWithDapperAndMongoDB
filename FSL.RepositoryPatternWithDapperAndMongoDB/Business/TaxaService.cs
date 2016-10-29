using FSL.RepositoryPatternWithDapperAndMongoDB.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Business
{
    public sealed class TaxaService
    {
        public static async Task<double> CalcularTaxa(int codigo, ITaxaRepository repository = null)
        {
            // se nao foi passado nenhum repositorio, se vira para achar um, no caso pega no DI
            repository = repository ?? GetRepository();

            var taxas = await repository.ListarTaxa(codigo);

            var valor = taxas.Sum(x => x.Valor);
            var fator = taxas.Sum(x => x.Fator);

            return ((valor * fator) * 0.002);
        }

        private static ITaxaRepository GetRepository()
        {
            return DependencyResolver.Current.GetService<ITaxaRepository>();
        }
    }
}