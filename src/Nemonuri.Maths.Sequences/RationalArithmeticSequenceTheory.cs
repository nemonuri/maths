namespace Nemonuri.Maths.Sequences;

public static partial class RationalArithmeticSequenceTheory
{
#if NET7_0_OR_GREATER
    /// <note>
    /// GetPseudoIndex(value, first, difference, 1/2) 과 이론적으로 동일
    /// </note>
    public static TNumber GetPseudoIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        var v1 = (value - first)/difference;
        var v2 = TNumber.One / (TNumber.One + TNumber.One);
        return v1 - v2;
    }

    public static TNumber GetPseudoIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference,
        TNumber leftTolerance
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        Guard.IsInRange(leftTolerance, TNumber.Zero, TNumber.One);

        var v1 = (value - first)/difference;
        var v2 = v1 + leftTolerance;
        return v2;
    }
#endif
}
