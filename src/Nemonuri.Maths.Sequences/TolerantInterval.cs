namespace Nemonuri.Maths.Sequences;

using RawStructs;

public class TolerantInterval<T> : ITolerantInterval<T>
{
    private readonly RawStructTolerantInterval<T> _rawStruct;

    public TolerantInterval
    (
        T leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,
        T leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,
        T rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,
        T rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection
    ) : this
    (
        new RawStructTolerantInterval<T>
        (
            leftToleranceBoundary,
            leftToleranceBoundaryClosedDirection,
            leftMainBoundary,
            leftMainBoundaryClosedDirection,
            rightMainBoundary,
            rightMainBoundaryClosedDirection,
            rightToleranceBoundary,
            rightToleranceBoundaryClosedDirection
        )
    )
    {}

    public TolerantInterval(RawStructTolerantInterval<T> rawStruct)
    {
        _rawStruct = rawStruct;
    }

    public ref readonly RawStructTolerantInterval<T> InnerRawStruct => ref _rawStruct;

    public T LeftToleranceBoundary => _rawStruct.LeftToleranceBoundary;
    public BoundaryClosedDirection LeftToleranceBoundaryClosedDirection => _rawStruct.LeftToleranceBoundaryClosedDirection;

    public T LeftMainBoundary => _rawStruct.LeftMainBoundary;
    public BoundaryClosedDirection LeftMainBoundaryClosedDirection => _rawStruct.LeftMainBoundaryClosedDirection;

    public T RightMainBoundary => _rawStruct.RightMainBoundary;
    public BoundaryClosedDirection RightMainBoundaryClosedDirection => _rawStruct.RightMainBoundaryClosedDirection;

    public T RightToleranceBoundary => _rawStruct.RightToleranceBoundary;
    public BoundaryClosedDirection RightToleranceBoundaryClosedDirection => _rawStruct.RightToleranceBoundaryClosedDirection;
}
