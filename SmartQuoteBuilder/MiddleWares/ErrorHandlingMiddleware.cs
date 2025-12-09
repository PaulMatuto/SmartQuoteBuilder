using System.Net;
using System.Text.Json;

namespace SmartQuoteBuilder.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                await HandleErrorAsync(context, ex);
            }
        }
        private static async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new
            {
                message = exception.Message,
                detail = "An unexpected error occurred.",
                status = 500
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
