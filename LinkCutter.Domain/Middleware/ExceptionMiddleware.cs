using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LinkCutter.Domain.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        if(exception is NegocioException ex)
        {
            context.Response.StatusCode = 422;
            var JsonResponse = JsonSerializer.Serialize(new
            {
                Code = ex.Codigo,
                ex.Message
            });

            await context.Response.WriteAsync(JsonResponse);
        }
        else if(exception is ErroTecnicoException errTec)
        {
            context.Response.StatusCode = 512;
            var JsonResponse = JsonSerializer.Serialize(new
            {
                Code = errTec.Codigo,
                errTec.Message
            });

            await context.Response.WriteAsync(JsonResponse);

        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var JsonResponse = JsonSerializer.Serialize(new
            {
                Messsage = "Erro interno no servidor"
            });

            await context.Response.WriteAsync(JsonResponse);

        }
    }
}
