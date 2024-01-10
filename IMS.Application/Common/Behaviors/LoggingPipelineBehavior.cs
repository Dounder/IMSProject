using MediatR;
using Microsoft.Extensions.Logging;

namespace IMS.Application.Common.Behaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        //Request
        logger.LogInformation(
            "Handling {Name}. {@Date}",
            requestName,
            DateTime.UtcNow);

        var result = await next();

        //Response
        logger.LogInformation(
            "CleanArchitecture Request: {Name} {@Request}. {@Date}",
            requestName,
            request,
            DateTime.UtcNow);

        return result;
    }
}