namespace Nemonuri.Maths.Sequences;

public interface ITolerantInterval<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    T LeftToleranceBoundary {get;}
    BoundaryClosedDirection LeftToleranceBoundaryClosedDirection {get;}

    T LeftMainBoundary {get;}
    BoundaryClosedDirection LeftMainBoundaryClosedDirection {get;}
    
    T RightMainBoundary {get;}
    BoundaryClosedDirection RightMainBoundaryClosedDirection {get;}

    T RightToleranceBoundary {get;}
    BoundaryClosedDirection RightToleranceBoundaryClosedDirection {get;}
}

public interface ITolerantInterval<T, TBoundary>
    where TBoundary : 
        IOneWayClosedBoundary<T>
#if NET9_0_OR_GREATER
        , allows ref struct
    where T : allows ref struct
#endif
{
    TBoundary LeftTolerance {get;}
    TBoundary LeftMain {get;}
    TBoundary RightMain {get;}
    TBoundary RightTolerance {get;}
}