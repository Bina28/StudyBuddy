
using System.Text.Json;
using Application.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Middleware;

public class ExeptionMiddleware(ILogger<ExeptionMiddleware> logger, IHostEnvironment env) : IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (ValidationException ex)
    {
      await HandleValidationExeption(ex, context);
    }
    catch (Exception ex)
    {

      await HandleExeption(context, ex);
    }
  }

  private async Task HandleExeption(HttpContext context, Exception ex)
  {
    logger.LogDebug(ex, ex.Message);
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    var response = env.IsDevelopment() ?
    new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace)
    : new AppException(context.Response.StatusCode, ex.Message, null);

    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    var json = JsonSerializer.Serialize(response, options);
    await context.Response.WriteAsync(json);
  }

  private static async Task HandleValidationExeption(ValidationException ex, HttpContext context)
  {
    var validationErrors = new Dictionary<string, string[]>();

    if (ex.Errors is not null)
    {
      foreach (var error in ex.Errors)
      {
        if (validationErrors.TryGetValue(error.PropertyName, out var existingErrors))
        {
          validationErrors[error.PropertyName] = existingErrors.Append(error.ErrorMessage).ToArray();
        }
        else
        {
          validationErrors[error.PropertyName] = [error.ErrorMessage];
        }
      }
    }
    context.Response.StatusCode = StatusCodes.Status400BadRequest;
    var validationProblemDetails = new ValidationProblemDetails(validationErrors)
    {
      Status = StatusCodes.Status400BadRequest,
      Type = "ValidationFailure",
      Title = "Validation Error",
      Detail = "One or moore validation errors has occured"
    };
    await context.Response.WriteAsJsonAsync(validationProblemDetails);
  }
}
