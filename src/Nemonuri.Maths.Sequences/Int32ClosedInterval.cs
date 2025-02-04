namespace Nemonuri.Maths.Sequences;

public readonly struct Int32ClosedInterval
{
    public Int32ClosedInterval(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start {get;}
    public int End {get;}

    public int Count => End + 1 - Start;
}
