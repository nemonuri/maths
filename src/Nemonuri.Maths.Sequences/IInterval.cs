namespace Nemonuri.Maths.Sequences;

public interface IInterval<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    T LeftBoundary {get;}
    BoundaryClosedDirection LeftBoundaryClosedDirection {get;}

    T RightBoundary {get;}
    BoundaryClosedDirection RightBoundaryClosedDirection {get;}
}

public interface IInterval<T, TBoundary>
    where TBoundary : 
        IOneWayClosedBoundary<T>
#if NET9_0_OR_GREATER
        , allows ref struct
    where T : allows ref struct
#endif
{
    TBoundary Left {get;}
    TBoundary Right {get;}
}

public class Interval<T, TBoundary>
    where TBoundary : 
        IOneWayClosedBoundary<T>
{

}