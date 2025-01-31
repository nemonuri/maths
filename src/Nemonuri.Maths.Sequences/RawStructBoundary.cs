namespace Nemonuri.Maths.Sequences;

#if NET7_0_OR_GREATER
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructBoundary<TNumber> where TNumber : IFloatingPoint<TNumber>
{
    public BoundaryKind BoundaryKind;
    public TNumber Value;
}
#endif