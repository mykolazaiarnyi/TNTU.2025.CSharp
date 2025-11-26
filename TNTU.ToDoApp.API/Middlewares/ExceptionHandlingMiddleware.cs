using TNTU.ToDoApp.Domain.Exceptions;

namespace TNTU.ToDoApp.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ItemNotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = "Item not found."
            });
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = "Something whent wrong."
            });
        }
    }
}
