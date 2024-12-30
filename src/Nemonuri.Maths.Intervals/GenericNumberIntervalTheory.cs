namespace Nemonuri.Maths.Intervals;

#if NET7_0_OR_GREATER
public static class GenericNumberIntervalTheory
{
    public static TNumber End<TNumber, TInterval>(this TInterval interval)
        where TNumber : INumber<TNumber>
        where TInterval : IInterval<TNumber>
    {
        return interval.Start + interval.Length;
    }

    public static bool IsElementOf<TNumber, TInterval>
    (
        this TInterval interval,
        TNumber number,
        EndpointKind leftEndpointKind = EndpointKind.Close,
        EndpointKind rightEndpointKind = EndpointKind.Open
    )
        where TNumber : INumber<TNumber>
        where TInterval : IInterval<TNumber>
    {
        TNumber end = interval.End<TNumber, TInterval>();
        return (leftEndpointKind, rightEndpointKind) switch 
        {
            (EndpointKind.Open, EndpointKind.Open) => interval.Start < number && number < end,
            (EndpointKind.Close, EndpointKind.Open) => interval.Start <= number && number < end,
            (EndpointKind.Open, EndpointKind.Close) => interval.Start < number && number <= end,
            (EndpointKind.Close, EndpointKind.Close) => interval.Start <= number && number <= end,
            _ => ThrowHelper.ThrowInvalidOperationException<bool>()
        };
    }

    public static void MoveForward<TNumber, TInterval>
    (
        this TInterval interval,
        TNumber number,
        EndpointKind leftEndpointKind = EndpointKind.Close,
        EndpointKind rightEndpointKind = EndpointKind.Open
    )
        where TNumber : INumber<TNumber>
        where TInterval : IInterval<TNumber>
    {
        
    }

    public static void GetUniformlyPartitionedIntervalSpan<TNumber, TInterval>
    (
        this TInterval interval,
        IntervalFactory<TNumber, TInterval> intervalFactory,
        Func<int, TNumber> intToNumberConverter,
        Span<TInterval> outPartitionSpan
    )
        where TNumber : struct, INumber<TNumber>
        where TInterval : struct, IInterval<TNumber>
    {
        TNumber outPartitionSpanLengthAsTNumber = intToNumberConverter.Invoke(outPartitionSpan.Length);
        TNumber partitionedIntervalLength = interval.Length / outPartitionSpanLengthAsTNumber;
        
        for (int i = 0; i < outPartitionSpan.Length; i++)
        {
            TNumber currentStart = interval.Start * intToNumberConverter.Invoke(i) / outPartitionSpanLengthAsTNumber;
            outPartitionSpan[i] = intervalFactory.Invoke(currentStart, TNumber.Min(partitionedIntervalLength, interval.Length - currentStart));
        }
    }

    public static void GetUniformlyPartitionedIntervalAndIndex<TNumber, TInterval>
    (
        this TInterval interval,
        int partitionedLength,
        TNumber number,
        IntervalFactory<TNumber, TInterval> intervalFactory,
        Func<int, TNumber> intToNumberConverter,
        Func<TNumber, int> numberToFlooredIntConverter,
        out TInterval outInterval,
        out int outIndex
    )
        where TNumber : struct, INumber<TNumber>
        where TInterval : struct, IInterval<TNumber>
    {
        TNumber partitionedLengthAsTNumber = intToNumberConverter.Invoke(partitionedLength);
        TNumber partitionedIntervalLength = interval.Length / partitionedLengthAsTNumber;

        TNumber divisionResult = (number - interval.Start) / partitionedIntervalLength;
        outIndex = numberToFlooredIntConverter.Invoke(divisionResult);
        outInterval = intervalFactory.Invoke(interval.Start + intToNumberConverter.Invoke(outIndex) * partitionedIntervalLength, partitionedIntervalLength);
    }

}

#endif
