namespace Nemonuri.Aggregation;

public static class AggregationTheory
{
    public static bool SetIfLess<TItem, TValue>
    (
        ref TItem aggregatingItem,
        ref TValue aggregatingValue,
        TItem sourceItem,
        TValue sourceValue
    )
        where TValue : IComparable<TValue>
    {
        if (aggregatingValue.CompareTo(sourceValue) > 0)
        {
            //aggregatingValue < sourceValue

            aggregatingItem = sourceItem;
            aggregatingValue = sourceValue;

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool SetIfMore<TItem, TValue>
    (
        ref TItem aggregatingItem,
        ref TValue aggregatingValue,
        TItem sourceItem,
        TValue sourceValue
    )
        where TValue : IComparable<TValue>
    {
        if (aggregatingValue.CompareTo(sourceValue) < 0)
        {
            //aggregatingValue > sourceValue

            aggregatingItem = sourceItem;
            aggregatingValue = sourceValue;

            return true;
        }
        else
        {
            return false;
        }
    }
}
