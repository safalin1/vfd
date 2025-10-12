namespace Vfd.Api.Services.Displays;

public interface IDisplayHardware : IDisposable
{
    int MaxLineLength { get; }
    void Initialize();
    void Write(byte[] buffer);
    void Write(string text);
}