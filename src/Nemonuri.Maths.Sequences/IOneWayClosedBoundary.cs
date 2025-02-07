namespace Nemonuri.Maths.Sequences;

public interface IOneWayClosedBoundary<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    T Anchor {get;}
    BoundaryClosedDirection ClosedDirection {get;}
}
