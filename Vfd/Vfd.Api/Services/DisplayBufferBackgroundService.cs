using Vfd.Api.Services.Displays;

namespace Vfd.Api.Services;

public class DisplayBufferBackgroundService : BackgroundService
{
    private readonly DisplayBuffer _displayBuffer;
    private readonly IDisplayHardware _hardware;
    private readonly IConfiguration _configuration;

    public DisplayBufferBackgroundService(
        DisplayBuffer displayBuffer,
        IDisplayHardware hardware,
        IConfiguration configuration)
    {
        _displayBuffer = displayBuffer;
        _hardware = hardware;
        _configuration = configuration;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _hardware.Initialize();
        return base.StartAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _hardware.Dispose();
        base.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var updateInterval = _configuration.GetValue<int>("Display:UpdateInterval");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _displayBuffer.Tick();
            await Task.Delay(updateInterval, stoppingToken);
        }
    }
}