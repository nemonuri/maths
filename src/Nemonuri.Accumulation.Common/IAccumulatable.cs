namespace Nemonuri.Accumulation;

public interface IAccumulatable<out T, TNumber>
{
    IAccumulator<T, TNumber> GetAccumulator();
}