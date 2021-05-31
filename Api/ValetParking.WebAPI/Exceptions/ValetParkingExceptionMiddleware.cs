using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ValetParking.WebApi.Exceptions
{
    public class PassExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///     Creates a new instance of the <see cref="ExceptionMiddleware" /> class
        /// </summary>
        /// <param name="next">Next delegate in the core pipeline</param>
        public PassExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///     Get the proper http code
        /// </summary>
        /// <param name="e">Exception thrown</param>
        /// <returns>It returns the http code</returns>
        private static int GetHttpCode(Exception e)
        {
            var code = (int)HttpStatusCode.InternalServerError;
            switch (e)
            {
                case UnauthorizedAccessException _:
                    code = (int)HttpStatusCode.Unauthorized;
                    break;
                case InvalidOperationException _:
                case ApiException _:
                    code = (int)HttpStatusCode.BadRequest;
                    break;

                case ApplicationException _:
                    code = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return code;
        }

        /// <summary>
        ///     It handles the exceptions
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="exception">Exception thrown</param>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var userInfo = context.User;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetHttpCode(exception);
            var upn = userInfo?.FindFirst(ClaimTypes.Upn)?.Value ?? "WebApiUser";

            //Logging here exception using Log4Net 

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        /// <summary>
        ///     It invokes Async the next delegate in core pipeline
        /// </summary>
        /// <param name="httpContext">Http context</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}
