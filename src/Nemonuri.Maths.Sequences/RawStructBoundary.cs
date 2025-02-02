namespace Nemonuri.Maths.Sequences;


[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructIndexBoundary<TNumber> where TNumber : IComparable<TNumber>
{
    public BoundaryKind BoundaryKind;
    public int AnchorIndex;
    public TNumber ResidualTolerance;

    public IndexContainingState IsContainingIndex()
    {

    }

    private readonly bool IsContainingIndexInMain(int otherIndex, CompareConditions compareConditions) =>
        CompareTheory.IsSatisfyingCompareConditions(AnchorIndex, otherIndex, compareConditions);

    private readonly bool IsContainingIndexInResidualTolerance(TNumber residual)
    {
        if (BoundaryKind == BoundaryKind.Close)
        {
            return residual.CompareTo(ResidualTolerance) <= 0;
        }
        else if (BoundaryKind == BoundaryKind.Open)
        {
            return residual.CompareTo(ResidualTolerance) < 0;
        }
        else
        {
            return ThrowHelper.ThrowInvalidOperationException<bool>();
        }
    }
}
