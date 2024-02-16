namespace GatherApp.Contracts.Dtos
{
    public class UserResponse
    {
        public int TotalPageCount { get; set; }
        public int TotalItemCount { get; set; }
        public IEnumerable<SingleUserResponse> Users { get; set; } = Enumerable.Empty<SingleUserResponse>();
    }
}
