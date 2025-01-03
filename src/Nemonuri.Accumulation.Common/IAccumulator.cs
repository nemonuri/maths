namespace Nemonuri.Accumulation;

public interface IAccumulator<out T, TNumber>
{
    T Current { get; }

    bool Accumulate(TNumber number);
}
