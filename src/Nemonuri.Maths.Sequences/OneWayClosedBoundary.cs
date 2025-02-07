namespace Nemonuri.Maths.Sequences;

public class OneWayClosedBoundary<T> : IOneWayClosedBoundary<T>
{
    public OneWayClosedBoundary(T anchor, BoundaryClosedDirection closedDirection)
    {
        Anchor = anchor;
        ClosedDirection = closedDirection;
    }

    public T Anchor { get; }
    public BoundaryClosedDirection ClosedDirection { get; }
}
