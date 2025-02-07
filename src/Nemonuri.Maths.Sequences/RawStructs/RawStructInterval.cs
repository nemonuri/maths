namespace Nemonuri.Maths.Sequences.RawStructs;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawStructInterval<T> : IInterval<T>
{
    public T LeftBoundary;
    public BoundaryClosedDirection LeftBoundaryClosedDirection;

    public T RightBoundary;
    public BoundaryClosedDirection RightBoundaryClosedDirection;

    public RawStructInterval
    (
        T leftBoundary,
        BoundaryClosedDirection leftBoundaryClosedDirection,
        T rightBoundary,
        BoundaryClosedDirection rightBoundaryClosedDirection
    )
    {
        LeftBoundary = leftBoundary;
        LeftBoundaryClosedDirection = leftBoundaryClosedDirection;
        RightBoundary = rightBoundary;
        RightBoundaryClosedDirection = rightBoundaryClosedDirection;
    }

    readonly T IInterval<T>.LeftBoundary => LeftBoundary;
    readonly BoundaryClosedDirection IInterval<T>.LeftBoundaryClosedDirection => LeftBoundaryClosedDirection;
    readonly T IInterval<T>.RightBoundary => RightBoundary;
    readonly BoundaryClosedDirection IInterval<T>.RightBoundaryClosedDirection => RightBoundaryClosedDirection;
}