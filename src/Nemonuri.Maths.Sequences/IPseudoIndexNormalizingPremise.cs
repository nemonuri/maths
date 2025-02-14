namespace Nemonuri.Maths.Sequences;

public interface IPseudoIndexNormalizingPremise<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    T NormalizePseudoIndex(T pseudoIndex);
}
