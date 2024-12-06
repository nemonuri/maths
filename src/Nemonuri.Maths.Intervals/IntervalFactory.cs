namespace Nemonuri.Maths.Intervals;

public delegate TInterval IntervalFactory<TNumber, TInterval>(TNumber start, TNumber length)
#if NET7_0_OR_GREATER
    where TNumber : INumber<TNumber>
    where TInterval : IInterval<TNumber>
#endif
;