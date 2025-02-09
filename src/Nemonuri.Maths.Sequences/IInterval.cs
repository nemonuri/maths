namespace Nemonuri.Maths.Sequences;

public interface IInterval<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
{
    IOneWayClosedBoundary<T> Left {get;}

    IOneWayClosedBoundary<T> Right {get;}
}
