namespace Nemonuri.Maths.Intervals;


public interface IInterval<TNumber>
#if NET7_0_OR_GREATER
    where TNumber : IInterval<TNumber>
#endif
{
    TNumber Start {get;}

    TNumber Length {get;}

    
}

