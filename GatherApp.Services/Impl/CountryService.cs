using GatherApp.Contracts.Dtos;
using GatherApp.Contracts.Responses;
using GatherApp.Repositories;
using GatherApp.Services.Extensions;
using System.Net;

namespace GatherApp.Services.Impl
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<CountryResponse> GetCountries()
        {
            var countries = _unitOfWork.CountryRepository.GetCountries();

            var response = new CountryResponse();
            response.Countries = countries.Select(e => new SingleCountryResponse
            {
                CountryId = e.Id,
                CountryName = e.Name
            }).ToList();

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.OK, "", response);
        }
    }
}
