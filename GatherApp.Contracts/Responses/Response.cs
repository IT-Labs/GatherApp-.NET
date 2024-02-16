using System.Net;

namespace GatherApp.Contracts.Responses
{
    public class Response<T> : IResponse<T>
    {

        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; } = default(T);
    }
}
