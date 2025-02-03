namespace Nemonuri.Maths.Sequences;


public struct RawStructIndexAndResidual<TNumber> where TNumber : IComparable<TNumber>
{
    public ResidualDirection ResidualDirection;
    public int Index;
    public TNumber Residual;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructIndexBoundary<TNumber> where TNumber : IComparable<TNumber>
{
    public BoundaryKind BoundaryKind;
    public int AnchorIndex;
    public TNumber ResidualTolerance;

    public RawStructIndexBoundary(BoundaryKind boundaryKind, int anchorIndex, TNumber residualTolerance)
    {
        BoundaryKind = boundaryKind;
        AnchorIndex = anchorIndex;
        ResidualTolerance = residualTolerance;
    }

    public readonly BoundaryHavingState GetHavingState(int index, TNumber residual, BoundaryDirection direction)
    {
        if (HasIndexInMain(index, direction.ToCompareConditions()))
        {
            return BoundaryHavingState.Main;
        }
        else if (HasIndexInResidualTolerance(residual))
        {
            return BoundaryHavingState.ResidualTolerance;
        }
        else
        {
            return BoundaryHavingState.None;
        }
    }

    public readonly bool HasIndexInMain(int otherIndex, CompareConditions compareConditions) =>
        CompareTheory.IsSatisfyingCompareConditions(AnchorIndex, otherIndex, compareConditions);

    public readonly bool HasIndexInResidualTolerance(TNumber residual)
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
