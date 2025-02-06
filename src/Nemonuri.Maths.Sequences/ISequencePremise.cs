namespace Nemonuri.Maths.Sequences;

public interface ISequencePremise<T> : ISuccessorPremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    bool TryGetItem(int index, [NotNullWhen(true)] out T? outItem);
}

public interface IBoundableSequencePremise<T> : ISequencePremise<T>
{
    int GetCount
    (
        T leftBoundary, 
        BoundaryClosedDirection leftBoundaryClosedDirection, 
        T rightBoundary, 
        BoundaryClosedDirection rightBoundaryClosedDirection
    );
}

public interface IInversibleSequencePremise<T> : ISequencePremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    bool TryGetIndex(T item, out int outIndex);
}