using System.Data.Entity.Infrastructure;
using System.Net;
using System.Text.Json;
using BatalhaDePokemons.Crosscutting.Exceptions;

namespace BatalhaDePokemons.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsyn(context, e);
        }
    }

    private static Task HandleExceptionAsyn(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            DbUpdateException => HttpStatusCode.Conflict,
            NotFoundException => HttpStatusCode.NotFound,
            InvalidArgumentException => HttpStatusCode.BadRequest,
            MaxAtaquesException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            message = exception.Message,
            status = (int)statusCode
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}