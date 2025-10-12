using Vfd.Api.CommandSetTables;
using Vfd.Api.Services.Displays;

namespace Vfd.Api.Services;

public class DisplayBuffer
{
    private readonly IDisplayHardware _displayHardware;
    private readonly IVfdCommandSetTable _commandSetTable;
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

    public DisplayBuffer(
        IDisplayHardware displayHardware,
        IVfdCommandSetTable commandSetTable)
    {
        _displayHardware = displayHardware;
        _commandSetTable = commandSetTable;
    }

    public void Tick()
    {
        var maxLineLength = _displayHardware.MaxLineLength;
        
        string top = _topLine.Length < maxLineLength
            ? _topLine.Trim().Center(maxLineLength)
            : string.Join("", _topLine.Skip(_topLineIndex).Take(maxLineLength)).PadRight(maxLineLength);
        
        string bottom = _bottomLine.Length < maxLineLength 
            ? _bottomLine.Trim().Center(maxLineLength)
            : string.Join("", _bottomLine.Skip(_bottomLineIndex).Take(maxLineLength)).PadRight(maxLineLength);
        
        _topLineIndex++;
        _bottomLineIndex++;

        if (_topLineIndex > _topLine.Length)
        {
            _topLineIndex = 0 - _topLine.Length / 2;
        }

        if (_bottomLineIndex > _bottomLine.Length)
        {
            _bottomLineIndex = 0 - _bottomLine.Length / 2;
        }

        _displayHardware.Write(_commandSetTable.MoveCursorToHome);
        _displayHardware.Write(top + bottom);
    }
}