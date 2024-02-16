using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Responses;
using System.Net;

namespace GatherApp.Services.Extensions
{
    public class CustomResponseExtension
    {
        private static Response<T> CreateResponse<T>(HttpStatusCode status, string message = "", T data = default!)
        {
            return new Response<T>
            {
                Status = status,
                Messages = new List<string>
                {
                    message,
                },
                Data = data,
            };
        }

        public static Response<T> GenericResponse<T>(HttpStatusCode status, string message)
        {
            return CreateResponse<T>(status, message);
        }

        // bad request
        public static Response<T> ResponseBadRequest<T>(string message)
        {
            return CreateResponse<T>(HttpStatusCode.BadRequest, message);
        }

        // internal server error
        public static Response<T> ResponseInternalServerError<T>(string message)
        {
            return CreateResponse<T>(HttpStatusCode.InternalServerError, message);
        }

        // unauthorized
        public static Response<T> ResponseUnauthorized<T>(string message)
        {
            return CreateResponse<T>(HttpStatusCode.Unauthorized, message);
        }

        // event not found
        public static Response<T> ResponseEventNotFound<T>()
        {
            return CreateResponse<T>(HttpStatusCode.NotFound, Errors.EventNotFound);
        }

        // user not found
        public static Response<T> ResponseUserNotFound<T>()
        {
            return CreateResponse<T>(HttpStatusCode.NotFound, Errors.UserNotFound);
        }

        // response data object
        public static Response<T> ResponseDataObject<T>(HttpStatusCode status, string message, T data)
        {
            return CreateResponse<T>(status, message, data: data);
        }
    }
}
