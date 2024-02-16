using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Responses;
using GatherApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GatherApp.API.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("countries")]
        public Response<CountryResponse> GetCountries()
        {
            return _countryService.GetCountries();
        }
    }
}
