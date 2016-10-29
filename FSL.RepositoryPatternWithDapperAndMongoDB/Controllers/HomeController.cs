using FSL.RepositoryPatternWithDapperAndMongoDB.Business;
using FSL.RepositoryPatternWithDapperAndMongoDB.Repository;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Controllers
{
    public class HomeController : Controller
    {
        private ITaxaRepository _repository;

        public HomeController()
        {

        }

        public HomeController(ITaxaRepository repository)
        {
            _repository = repository;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            // Exemplos de chamadas paralelas usando diferentes repositorios

            // Servico se vira para achar o repositorio
            var calculoA = TaxaService.CalcularTaxa(1);

            // Servico recebe o repositorio via DI
            var calculoB = TaxaService.CalcularTaxa(1, _repository);

            // Servico recebe o repositorio especifico do mongodb
            var calculoC = TaxaService.CalcularTaxa(1, new MongoDBTaxaRepository());

            await Task.WhenAll(calculoA, calculoB, calculoC);
            
            return View();
        }
    }
}