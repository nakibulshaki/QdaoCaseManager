using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Serilog;

namespace QdaoCaseManager.Middlewares;
public class GlobalExceptionHandler (): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var details = new ProblemDetails()
        {
            Instance = httpContext.Request.Path,
            Status = httpContext.Response.StatusCode,
            Title = exception.Message,

        };
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken: cancellationToken);
        Log.Error(details.ToJson());
        return true;
    }
}

