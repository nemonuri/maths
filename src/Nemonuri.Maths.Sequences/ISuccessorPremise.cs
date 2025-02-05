namespace Nemonuri.Maths.Sequences;

public interface ISuccessorPremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    bool TryGetSuccessor(T? value, [NotNullWhen(true)] out T? outSuccessor);
}
