using AutoMapper;
using System.Net;

namespace Students.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly IMapper mapper;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        //type exception handler middleware
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                var errorId = Guid.NewGuid();
                //log this exception
                logger.LogError(e, $"{errorId} : {e.Message}");
                //return a custome response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something gone wrong"
                };
                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
