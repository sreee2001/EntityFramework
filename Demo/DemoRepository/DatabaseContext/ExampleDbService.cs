using System.ComponentModel.Composition;
using Repository.Interfaces;
using Repository.Services;

namespace DemoRepository.DatabaseContext
{
    [Export(typeof(IDbService))]
    public class ExampleDbService : DbServiceBase<ExampleDbContext>
    {

    }

}
