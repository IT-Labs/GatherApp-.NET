using System.Net;

namespace GatherApp.Contracts.Responses
{
    public interface IResponse<T>: IResponse
    { 
        T Data { get; set; }
    }
}
