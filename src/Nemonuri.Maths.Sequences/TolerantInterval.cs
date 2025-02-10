namespace Nemonuri.Maths.Sequences;

public class TolerantInterval<T> : ITolerantInterval<T>
{
    public TolerantInterval
    (
        IOneWayClosedBoundary<T> leftTolerance, 
        IOneWayClosedBoundary<T> leftMain, 
        IOneWayClosedBoundary<T> rightMain, 
        IOneWayClosedBoundary<T> rightTolerance
    )
    {
        LeftTolerance = leftTolerance;
        LeftMain = leftMain;
        RightMain = rightMain;
        RightTolerance = rightTolerance;
    }

    public IOneWayClosedBoundary<T> LeftTolerance { get; }
    public IOneWayClosedBoundary<T> LeftMain { get; }
    public IOneWayClosedBoundary<T> RightMain { get; }
    public IOneWayClosedBoundary<T> RightTolerance { get; }
}
