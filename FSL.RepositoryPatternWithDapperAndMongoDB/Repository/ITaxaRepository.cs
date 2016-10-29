using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Repository
{
    public interface ITaxaRepository : IDisposable
    {
        Task<IEnumerable<Models.Taxa>> ListarTaxa(int codigo);
    }
}