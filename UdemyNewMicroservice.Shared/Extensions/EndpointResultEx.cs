using Microsoft.AspNetCore.Http;
using System.Net;

namespace UdemyNewMicroservice.Shared.Extensions
{
    public static class EndpointResultEx
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
            return result.Status switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),
                HttpStatusCode.Created => Results.Created(string.Empty, result.Data),
                HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
                HttpStatusCode.Unauthorized => Results.Unauthorized(),
                HttpStatusCode.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
                _ => Results.Problem(result.Fail!)
            };
        }

        public static IResult ToGenericResult(this ServiceResult result)
        {
            return result.Status switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                _ => Results.Problem(result.Fail!)
            };
        }
    }
}
