namespace GatherApp.Contracts.Dtos
{
    public class CountryResponse
    {
        public IEnumerable<SingleCountryResponse> Countries { get; set; } = Enumerable.Empty<SingleCountryResponse>();
    }
}
