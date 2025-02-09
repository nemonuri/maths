namespace Nemonuri.Maths.Sequences;

public class Interval<T, TBoundary> : IInterval<T, TBoundary>
    where TBoundary : IOneWayClosedBoundary<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    public Interval(TBoundary left, TBoundary right)
    {
        Left = left;
        Right = right;
    }

    public TBoundary Left {get;}

    public TBoundary Right {get;}
}