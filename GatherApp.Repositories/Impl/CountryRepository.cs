using GatherApp.Contracts.Entities;
using GatherApp.DataContext;

namespace GatherApp.Repositories.Impl
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(GatherAppContext gatherAppContext) : base(gatherAppContext)
        {
        }

        public IEnumerable<Country> GetCountries()
        {
            return GetAll().OrderBy(e => e.Name);
        }
    }
}
