using FluentValidation.Results;
using GatherApp.Contracts.Responses;
using System.Net;

namespace GatherApp.Services.Extensions
{
	static class ValidationErrors
	{
        public static Response<T> CreateErrorResponseWithGenericType<T>(this ValidationResult result, HttpStatusCode statusCode) where T : class
        {
            List<string> errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            return new Response<T>
            {
                Status = statusCode,
                Messages = [.. errorMessages]
            };
        }

		public static Response<string> CreateErrorsResponseWithString(this ValidationResult result, HttpStatusCode statusCode) 
		{
            List<string> errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            return new Response<string>
            {
                Status = statusCode,
                Messages = [.. errorMessages]
            };


        }

        public static Response<bool> CreateErrorsResponsesWithBool(this ValidationResult result, HttpStatusCode statusCode)
        {
            List<string> errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            return new Response<bool>
            {
                Status = statusCode,
                Messages = [.. errorMessages]
            };


        }
    }
}
