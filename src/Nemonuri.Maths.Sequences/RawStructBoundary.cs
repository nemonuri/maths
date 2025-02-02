namespace Nemonuri.Maths.Sequences;


[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructIndexBoundary<TNumber> where TNumber : IComparable<TNumber>
{
    public BoundaryKind BoundaryKind;
    public int AnchorIndex;
    public TNumber ResidualTolerance;
}

[Flags]
public enum CompareConditions
{
    None = 0,
    Less = 1 << 0,
    Equal = 1 << 1,
    Greater = 1 << 2
}