namespace Nemonuri.Maths.Sequences;

public interface IIndexGetableSequencePremise<T> : ISequencePremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    bool TryGetIndex(T item, out int outIndex);
}