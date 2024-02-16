using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface ICountryService
    {
        Response<CountryResponse> GetCountries(); 
    }
}
