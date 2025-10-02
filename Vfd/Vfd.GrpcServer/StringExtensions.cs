namespace Vfd.GrpcServer;

public static class StringExtensions
{
    public static string Center(this string input, int totalLength)
    {
        int length = input.Length;

        return input
            .PadLeft((totalLength - length) / 2 + length)
            .PadRight(totalLength);
    }

    public static string ShiftRight(this string s, int count)
    {
        return s.Remove(0, count) + s.Substring(0, count);
    }
}