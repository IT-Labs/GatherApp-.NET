namespace GatherApp.Contracts.Requests
{
    public class GetByPageRequest : IRequest
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 9;
    }

}
