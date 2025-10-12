namespace Vfd.Api.CommandSetTables;

public interface IVfdCommandSetTable
{
    byte[] MoveCursorRight { get; }
    byte[] MoveCursorLeft { get; }
    byte[] MoveCursorUp { get; }
    byte[] MoveCursorDown { get; }
    byte[] MoveCursorToRightEnd { get; }
    byte[] MoveCursorToLeftEnd { get; }
    byte[] MoveCursorToHome { get; }
    byte[] MoveCursorToBottom { get; }
    byte[] ClearDisplay { get; }
    byte[] ClearCursorLine { get; }
    byte[] SetOverwriteMode { get; }
    byte[] SetVerticalScrollMode { get; }
    byte[] SetHorizontalScrollMode { get; }
    byte[] InitialiseDisplay { get; }
    byte[] SelfTest { get; }
    byte[] MoveCursorToPosition(int x, int y);
    byte[] SetOrCancelCursorDisplay(bool visible);
    byte[] SelectCodeTable(byte table);
    byte[] SelectCharSet(byte set);
    byte[] SetCancelReverseCharacter(bool cancel);
    byte[] SetDisplayTimeCounter(int h, int m);
    byte[] Blink(int interval);
    byte[] Brightness(int brightness);
}