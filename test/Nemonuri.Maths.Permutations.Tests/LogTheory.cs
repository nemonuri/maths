namespace Nemonuri.Maths.Permutations.Tests;

internal static class LogTheory
{
    public static string ConvertSpanToLogString<T>(ReadOnlySpan<T> source)
    {
        return $"[{string.Join(',', source.ToArray())}]";
    }
}