using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Play.Catalog.Application.Common.Behaviours
{

    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        
        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
            
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var properties = new Dictionary<string, string>
                {
                    { "RequestName", requestName },
                    { "ElapsedMilliseconds", elapsedMilliseconds.ToString() }
                };

                _logger.LogWarning("Play.Catalog.Application Long Running Request: {RequestName} ({ElapsedMilliseconds} milliseconds) {@Request} {prperties}", requestName, elapsedMilliseconds, request, properties);
            }

            return response;
        }
    }
}