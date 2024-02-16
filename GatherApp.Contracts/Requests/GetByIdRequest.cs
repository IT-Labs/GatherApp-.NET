namespace GatherApp.Contracts.Requests
{
    public class GetByIdRequest : IRequest
    {
        public string Id { get; set; }
    }
}
