namespace Nemonuri.Maths.Sequences;

public interface ITolerantInterval<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    IOneWayClosedBoundary<T> LeftTolerance {get;}
    IOneWayClosedBoundary<T> LeftMain {get;}
    IOneWayClosedBoundary<T> RightMain {get;}
    IOneWayClosedBoundary<T> RightTolerance {get;}
}