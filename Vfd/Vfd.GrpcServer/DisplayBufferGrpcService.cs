using Grpc.Core;
using Vfd.GrpcServer.Services.Displays;

namespace Vfd.GrpcServer;

public class DisplayBufferGrpcService : DisplayBuffer.DisplayBufferBase
{
    private readonly ILogger<DisplayBufferGrpcService> _logger;
    private readonly IDisplayHardware _displayHardware;

    public DisplayBufferGrpcService(
        ILogger<DisplayBufferGrpcService> logger,
        IDisplayHardware displayHardware)
    {
        _logger = logger;
        _displayHardware = displayHardware;
    }

    public override Task<ClearResponse> Clear(ClearRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Clearing display buffers");
        _displayHardware.Clear();

        return Task.FromResult(new ClearResponse());
    }

    public override Task<SetBufferResponse> SetBuffer(SetBufferRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Setting buffers");

        _displayHardware.TopLine = request.TopLine;
        _displayHardware.BottomLine = request.BottomLine;
        
        return Task.FromResult(new SetBufferResponse());
    }
}