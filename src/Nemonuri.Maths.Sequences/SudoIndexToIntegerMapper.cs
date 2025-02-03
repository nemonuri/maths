namespace Nemonuri.Maths.Sequences;

public delegate TNumber SudoIndexToIntegerMapper<TNumber>
(
    TNumber sudoIndex,
    out TNumber outResidual
)
#if NET9_0_OR_GREATER
where TNumber : allows ref struct
#endif
;
