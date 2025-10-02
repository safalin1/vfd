using System.IO.Ports;

namespace Vfd.GrpcServer.Services.Displays;

public class HPVfd220DisplayHardware : IDisplayHardware
{
    private const int _maxLineChars = 20;
    
    private static readonly byte[] _initializeScreen = [0x1B, 0x40];
    private static readonly byte[] _clearScreen = [0x0C];
    private static readonly byte[] _clearCursorLine = [0x18];
    private static readonly byte[] _selfTest = [0x1F, 0x40];
    private static readonly byte[] _setVerticalScroll = [0x1F, 0x02];
    private static readonly byte[] _moveCursorToHome = [0x1F, 0x24, 0x1, 0x1];
    private static readonly byte[] _origBuffer = [0x1B, 0x74, 0x0];
    
    private readonly SerialPort _serialPort;
    
    private string _topLine = string.Empty;
    private int _topLineIndex;
    
    private string _bottomLine = string.Empty;
    private int _bottomLineIndex;

    public string? TopLine
    {
        get => _topLine;
        set
        {
            _topLine = value ?? string.Empty;
            _topLineIndex = 0;
        }
    }

    public string? BottomLine
    {
        get => _bottomLine;
        set
        {
            _bottomLine = value ?? string.Empty;
            _bottomLineIndex = 0;
        }
    }

    public HPVfd220DisplayHardware(IConfiguration configuration)
    {
        _serialPort = new SerialPort(configuration.GetValue<string>("Display:ComPort"))
        {
            WriteTimeout = 5000,
            BaudRate = 9600
        };
    }
    
    public void Dispose()
    {
        _serialPort.Close();
        _serialPort.Dispose();
    }

    public void Initialize()
    {
        _serialPort.Open();
        Clear();
    }

    public void Clear()
    {
        _serialPort.Write(_clearScreen, 0, _clearScreen.Length);
    }

    public void Blink(int duration)
    {
        byte[] buffer = [0x1F, 0x45, (byte)duration];
        _serialPort.Write(buffer, 0, buffer.Length);
    }

    public void Draw()
    {
        string top = _topLine.Length < _maxLineChars
            ? _topLine.Trim().Center(_maxLineChars)
            : string.Join("", _topLine.Skip(_topLineIndex).Take(_maxLineChars));
        
        string bottom = _bottomLine.Length < _maxLineChars 
            ? _bottomLine.Trim().Center(_maxLineChars)
            : string.Join("", _bottomLine.Skip(_bottomLineIndex).Take(_maxLineChars));
        
        // _serialPort.Write(_clearBuffer, 0, 2);
        // _serialPort.Write(_origBuffer, 0, 3);
        _serialPort.Write(_moveCursorToHome, 0, _moveCursorToHome.Length);
        // _serialPort.Write($"{top}{bottom}");
        
        WriteTextAtPosition(1, 1, top);
        WriteTextAtPosition(1, 2, bottom);
        
        _topLineIndex++;
        _bottomLineIndex++;

        if (_topLineIndex < 0)
        {
            _topLineIndex = 0 - _topLine.Length;
        }

        if (_bottomLineIndex < 0)
        {
            _bottomLineIndex = 0 - _bottomLine.Length;
        }
    }
    
    private void SetCursorPosition(int x, int y)
    {
        byte[] buffer = [0x1F, 0x24, (byte)x, (byte)y];
        
        _serialPort.Write(buffer, 0, buffer.Length);
    }

    private void WriteTextAtPosition(int x, int y, string text)
    {
        SetCursorPosition(x, y);
        _serialPort.Write(text);
    }
}