namespace Nemonuri.Maths.Intervals;

public interface IUniformlyPartitionedInterval<TNumber, TInterval>
#if NET7_0_OR_GREATER
    where TNumber : INumber<TNumber>
    where TInterval : IInterval<TNumber>
#endif
{

}

