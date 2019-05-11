using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dfreeze.Services.Background
{
    public class FreezerInfoUpdater : IHostedService, IDisposable
    {
        private readonly IFreezerStateHolder _stateHolder;
        private readonly IFreezeService _freezerService;
        private readonly ILogger _logger;
        private Timer _timer;

        public FreezerInfoUpdater(IFreezerStateHolder stateHolder,
            IFreezeService freezerService,
            ILogger<FreezerInfoUpdater> logger)
        {
            _stateHolder = stateHolder;
            _freezerService = freezerService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("FreezerInfoUpdater is starting.");
            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private async void DoWorkAsync(object state)
        {
            _logger.LogInformation("FreezerInfoUpdater is working.");
            try
            {
                var freezers = await _freezerService.GetFreezersAsync();
                _stateHolder.UpdateState(freezers);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("FreezerInfoUpdater is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}