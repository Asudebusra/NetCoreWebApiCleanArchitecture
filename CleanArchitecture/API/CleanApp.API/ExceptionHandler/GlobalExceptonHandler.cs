using App.Application;
using App.Services;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CleanApp.API.ExceptionHandler
{
    public class GlobalExceptonHandler : IExceptionHandler//burdan sonra response u doldurmam lazım response belli olacak
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorAsDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

            return true;
        }
    }
}
