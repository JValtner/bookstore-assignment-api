using System.Text.Json;
using BookstoreApplication.Exceptions;

namespace BookstoreApplication.Settings
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        public ExceptionHandlingMiddleware() { }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest, // Važno! Ako se desi izuzetak BadRequestException, statusni kod se postavlja na 400. BadRequestException je klasa koju smo prethodno napravili.
                NotFoundException => StatusCodes.Status404NotFound, // Važno! Ako se desi izuzetak NotFoundException, statusni kod se postavlja na 404. NotFoundException je klasa koju smo prethodno napravili.
                CascadeDeleteException => StatusCodes.Status409Conflict, // Važno! Ako se desi izuzetak CascadeDeleteException, statusni kod se postavlja na 409. CascadeDeleteException je klasa koju smo prethodno napravili.
                _ => StatusCodes.Status500InternalServerError // Važno! Ako se desi bilo koji drugi izuzetak, statusni kod se postavlja na 500.
            };
            var response = new { error = exception.Message };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
