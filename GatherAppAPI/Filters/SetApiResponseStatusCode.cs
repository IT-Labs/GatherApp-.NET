using GatherApp.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GatherApp.API.Filters
{
    //Inheriting from ActionFilterAttribute, used to add extra logic before or after action methods
    public class SetApiResponseStatusCode : ActionFilterAttribute
    {
        //Overriding the method OnResultExecuting to set the API status code in HttpContext, based on the status code of the action method 
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //Checking if the property Value of ObjectResult class (returns object from action method context.Result) is implementing the IResponse interface
            //This enables the API to return responses with specific status code
            if ((context.Result as ObjectResult)?.Value is IResponse response)
            {
                context.HttpContext.Response.StatusCode = (int)response.Status;
            }
        }
    }
}