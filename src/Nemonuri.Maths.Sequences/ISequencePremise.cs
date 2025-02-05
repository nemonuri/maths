namespace Nemonuri.Maths.Sequences;

public interface ISequencePremise<T> : ISuccessorPremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    int Count {get;}

    bool TryGetItem(int index, [NotNullWhen(true)] out T? outItem);
}
