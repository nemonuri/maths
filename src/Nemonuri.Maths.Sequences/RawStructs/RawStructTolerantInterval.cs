namespace Nemonuri.Maths.Sequences.RawStructs;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructTolerantInterval<T>
{
    public T LeftToleranceBoundary;
    public BoundaryClosedDirection LeftToleranceBoundaryClosedDirection;

    public T LeftMainBoundary;
    public BoundaryClosedDirection LeftMainBoundaryClosedDirection;

    public T RightMainBoundary;
    public BoundaryClosedDirection RightMainBoundaryClosedDirection;

    public T RightToleranceBoundary;
    public BoundaryClosedDirection RightToleranceBoundaryClosedDirection;

    public RawStructTolerantInterval
    (
        T leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,
        T leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,
        T rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,
        T rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection
    )
    {
        LeftToleranceBoundary = leftToleranceBoundary;
        LeftToleranceBoundaryClosedDirection = leftToleranceBoundaryClosedDirection;
        LeftMainBoundary = leftMainBoundary;
        LeftMainBoundaryClosedDirection = leftMainBoundaryClosedDirection;
        RightMainBoundary = rightMainBoundary;
        RightMainBoundaryClosedDirection = rightMainBoundaryClosedDirection;
        RightToleranceBoundary = rightToleranceBoundary;
        RightToleranceBoundaryClosedDirection = rightToleranceBoundaryClosedDirection;
    }
}
