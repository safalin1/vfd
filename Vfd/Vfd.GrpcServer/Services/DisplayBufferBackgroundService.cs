using Vfd.GrpcServer.Services.Displays;

namespace Vfd.GrpcServer.Services;

public class DisplayBufferBackgroundService : BackgroundService
{
    private readonly ILogger<DisplayBufferBackgroundService> _logger;
    private readonly IDisplayHardware _displayDispatcher;

    public DisplayBufferBackgroundService(
        ILogger<DisplayBufferBackgroundService> logger,
        IDisplayHardware displayDispatcher)
    {
        _logger = logger;
        _displayDispatcher = displayDispatcher;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _displayDispatcher.Initialize();
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _displayDispatcher.Clear();
        _displayDispatcher.Dispose();
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information) && _displayDispatcher.TopLine == null)
            {
                _displayDispatcher.TopLine = "The current time is...";
                _displayDispatcher.BottomLine = DateTimeOffset.Now.ToString();
            }

            _displayDispatcher.Draw();
            await Task.Delay(1000, stoppingToken);
        }
    }
}