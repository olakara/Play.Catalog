using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Play.Catalog.Application.Common.Behaviours
{

    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly int _maxConcurrentRequests;

        public PerformanceBehaviour(ILogger<TRequest> logger, int maxConcurrentRequests)
        {
            _logger = logger;
            _maxConcurrentRequests = maxConcurrentRequests;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var response = await next();
            timer.Stop();

            var elapsedMilliseconds = timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var properties = new Dictionary<string, string>
                {
                    { "RequestName", requestName },
                    { "ElapsedMilliseconds", elapsedMilliseconds.ToString() }
                };

                _logger.LogWarning("Play.Catalog.Application Long Running Request: {RequestName} ({ElapsedMilliseconds} milliseconds) {@Request}", requestName, elapsedMilliseconds, request, properties);
            }

            return response;
        }
    }
}