namespace Nemonuri.Maths.Sequences;

public delegate TNumber PseudoIndexNormalizer<TNumber>
(
    TNumber pseudoIndex,
    out TNumber outResidual
)
#if NET9_0_OR_GREATER
where TNumber : allows ref struct
#endif
;
