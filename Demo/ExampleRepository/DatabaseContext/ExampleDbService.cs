using System.ComponentModel.Composition;
using ExampleRepository.DatabaseContext;
using Repository.Interfaces;
using Repository.Services;

namespace ExampleRepository.DatabaseContext
{
    //[Export(typeof(IDbService))]
    public class ExampleDbService : DbServiceBase<ExampleDbContext>
    {

    }

}
