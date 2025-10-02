namespace Vfd.GrpcServer.Services.Displays;

public interface IDisplayHardware : IDisposable
{
    string? TopLine { get; set; }
    string? BottomLine { get; set; }
    void Initialize();
    void Clear();
    void Draw();
}