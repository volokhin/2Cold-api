using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dfreeze.Services.Background
{
    public class FreezerBackgroundWorker : BackgroundService
    {
        private readonly IFreezerStateHolder _stateHolder;
        private readonly IFreezerTasksProcessor _processor;
        private readonly ILogger _logger;

        public FreezerBackgroundWorker(IFreezerStateHolder stateHolder,
            IFreezerTasksProcessor processor,
            ILogger<FreezerInfoUpdater> logger)
        {
            _stateHolder = stateHolder;
            _processor = processor;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var task = base.StartAsync(cancellationToken);
            _logger.LogInformation("FreezerBackgroundWorker is starting.");
            return task;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            var task = base.StartAsync(cancellationToken);
            _logger.LogInformation("FreezerBackgroundWorker is stopping.");
            return task;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("FreezerBackgroundWorker is working.");
                    var result = await _processor.ProcessNextTaskAsync();
                    if (result != null)
                    {
                        _stateHolder.UpdateState(result);
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, ex.Message);
                }
            }
        }
    }
}