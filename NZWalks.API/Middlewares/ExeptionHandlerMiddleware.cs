using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExeptionHandlerMiddleware
    {
        private readonly Logger<ExeptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExeptionHandlerMiddleware(Logger<ExeptionHandlerMiddleware> logger , RequestDelegate next)
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
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log this exeption
                logger.LogError(ex ,$"{errorId} : {ex.Message}");

                // Return custom error response

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessager = "Somthing went wrong! we are looking into resolving this "
                };

                await httpContext.Response.WriteAsJsonAsync(error); 
            }
        }

    }
}
