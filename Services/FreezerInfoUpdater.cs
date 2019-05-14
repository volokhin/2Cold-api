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
            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromMinutes(60));
            return Task.CompletedTask;
        }

        private async void DoWorkAsync(object state)
        {
            try
            {
                var freezersOn5 = await _freezerService.GetFreezersAsync(floor: 5);
                _stateHolder.UpdateState(freezersOn5);
                var freezersOn8 = await _freezerService.GetFreezersAsync(floor: 8);
                _stateHolder.UpdateState(freezersOn8);
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