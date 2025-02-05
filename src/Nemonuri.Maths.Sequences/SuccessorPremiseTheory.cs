namespace Nemonuri.Maths.Sequences;

public static class SuccessorPremiseTheory
{
    public static bool GetSuccessor<T>(this ISequencePremise<T> self, ref T current)
        where T : struct
    {
        Guard.IsNotNull(self);

        return self.TryGetSuccessor(current, out current);
    }

    public static bool GetSuccessor<T>(this ISequencePremise<T> self, T? current)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        return self.TryGetSuccessor(current, out _);
    }
}
