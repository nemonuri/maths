namespace Nemonuri.Maths.Sequences;

public class PseudoIndexNormalizingPremise<T> : IPseudoIndexNormalizingPremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    private readonly Func<T, T> _innerFunc;

    public PseudoIndexNormalizingPremise(Func<T, T> innerFunc)
    {
        Guard.IsNotNull(innerFunc);
        _innerFunc = innerFunc;
    }

    public T NormalizePseudoIndex(T pseudoIndex)
    {
        return _innerFunc.Invoke(pseudoIndex);
    }
}