using System.Net;

namespace GatherApp.Contracts.Responses
{
    public interface IResponse
    { 
        HttpStatusCode Status { get; set; }

        List<string> Messages { get; set; }

    }
}

