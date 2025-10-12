using System.IO.Ports;
using Vfd.Api.CommandSetTables;

namespace Vfd.Api.Services.Displays;

public class SerialPortDisplayHardware : IDisplayHardware
{
    private readonly IVfdCommandSetTable _commandSetTable;
    private readonly SerialPort _serialPort;
    
    public SerialPortDisplayHardware(
        SerialPort serialPort,
        IVfdCommandSetTable commandSetTable)
    {
        _commandSetTable = commandSetTable;
        _serialPort = serialPort;
    }
    
    public void Dispose()
    {
        Write(_commandSetTable.ClearDisplay);
        
        _serialPort.Close();
        _serialPort.Dispose();
        
        GC.SuppressFinalize(this);
    }

    public int MaxLineLength => 20;

    public void Initialize()
    {
        _serialPort.Open();
        
        Write(_commandSetTable.InitialiseDisplay);
    }

    public void Write(byte[] buffer)
    {
        _serialPort.Write(buffer, 0, buffer.Length);
    }

    public void Write(string text)
    {
        _serialPort.Write(text);
    }
}