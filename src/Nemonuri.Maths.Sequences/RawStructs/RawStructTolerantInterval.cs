namespace Nemonuri.Maths.Sequences.RawStructs;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructTolerantInterval<T> : ITolerantInterval<T>
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

    readonly T ITolerantInterval<T>.LeftToleranceBoundary => LeftToleranceBoundary;
    readonly BoundaryClosedDirection ITolerantInterval<T>.LeftToleranceBoundaryClosedDirection => LeftToleranceBoundaryClosedDirection;
    readonly T ITolerantInterval<T>.LeftMainBoundary => LeftMainBoundary;
    readonly BoundaryClosedDirection ITolerantInterval<T>.LeftMainBoundaryClosedDirection => LeftMainBoundaryClosedDirection;
    readonly T ITolerantInterval<T>.RightMainBoundary => RightMainBoundary;
    readonly BoundaryClosedDirection ITolerantInterval<T>.RightMainBoundaryClosedDirection => RightMainBoundaryClosedDirection;
    readonly T ITolerantInterval<T>.RightToleranceBoundary => RightToleranceBoundary;
    readonly BoundaryClosedDirection ITolerantInterval<T>.RightToleranceBoundaryClosedDirection => RightToleranceBoundaryClosedDirection;
}
