using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Entities;

namespace GatherApp.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        public IEnumerable<Country> GetCountries();
    }
}
