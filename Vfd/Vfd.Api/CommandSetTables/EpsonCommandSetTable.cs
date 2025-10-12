namespace Vfd.Api.CommandSetTables;

public class EpsonCommandSetTable : IVfdCommandSetTable
{
    public byte[] MoveCursorRight { get; } = [0x09];
    public byte[] MoveCursorLeft { get; } = [0x08];
    public byte[] MoveCursorUp { get; } = [0x1F, 0x0A];
    public byte[] MoveCursorDown { get; } = [0x0A];
    public byte[] MoveCursorToRightEnd { get; } = [0x1F, 0x0D];
    public byte[] MoveCursorToLeftEnd { get; } = [0x0E];
    public byte[] MoveCursorToHome { get; } = [0x0B];
    public byte[] MoveCursorToBottom { get; } = [0x1F, 0x42];
    public byte[] MoveCursorToPosition(int x, int y) => [0x1F, 0x24, (byte)x, (byte)y];

    public byte[] ClearDisplay { get; } = [0x0C];
    public byte[] ClearCursorLine { get; } = [0x18];
    public byte[] SetOrCancelCursorDisplay(bool visible) => [0x1F, 0x43, visible ? (byte)0x01 : (byte)0x00];
    public byte[] SelectCodeTable(byte table) => [0x1B, 0x74, table];
    public byte[] SelectCharSet(byte set) => [0x1B, 0x52, set];
    public byte[] SetCancelReverseCharacter(bool cancel) => [0x1F, 0x72, (byte)(cancel ? 0x01 : 0x00)];
    public byte[] SetOverwriteMode { get; } = [0x1F, 0x01];
    public byte[] SetVerticalScrollMode { get; } = [0x1F, 0x02];
    public byte[] SetHorizontalScrollMode { get; } = [0x1F, 0x03];

    public byte[] SetDisplayTimeCounter(int h, int m) => [0x1F, 0x54, (byte)h, (byte)m];
    public byte[] Blink(int interval) => [0x1F, 0x45, (byte)interval];
    public byte[] Brightness(int brightness) => [0x1F, 0x58, (byte)brightness];
    public byte[] InitialiseDisplay { get; } = [0x1B, 0x40];
    public byte[] SelfTest { get; } = [0x1F, 0x40];
}