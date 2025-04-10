using System.Data.Entity.Infrastructure;
using System.Net;
using System.Text.Json;
using BatalhaDePokemons.Crosscutting.Exceptions;
using BatalhaDePokemons.Crosscutting.Exceptions.Shared;

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
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            DbUpdateException => HttpStatusCode.Conflict,
            NotFoundException => HttpStatusCode.NotFound,
            InvalidArgumentException => HttpStatusCode.BadRequest,
            DomainException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            message = exception.Message,
            status = (int)statusCode,
            error = exception.GetType().Name
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}