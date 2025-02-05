namespace Nemonuri.Maths.Sequences;

public static class NormalizedIndexTheory
{
    public static TPseudoIndex GetNormalizedIndex<TRaw, TPseudoIndex>
    (
        TRaw raw,
        Func<TRaw, TPseudoIndex> rawToPseudoIndexMapping,
        Func<TPseudoIndex, TPseudoIndex>? pseudoIndexNormalizer
    )
    {
        Guard.IsNotNull(rawToPseudoIndexMapping);

        var v1 = rawToPseudoIndexMapping.Invoke(raw);
        return pseudoIndexNormalizer is null ? v1 : pseudoIndexNormalizer.Invoke(v1);
    }

    public static TPseudoIndex GetNormalizedIndex<TRaw, TPseudoIndex, TArg1>
    (
        TRaw raw,
        Func<TRaw, TArg1?, TPseudoIndex> rawToPseudoIndexMapping,
        TArg1? rawToPseudoIndexMappingArg,
        Func<TPseudoIndex, TPseudoIndex>? pseudoIndexNormalizer
    )
    {
        Guard.IsNotNull(rawToPseudoIndexMapping);

        var v1 = rawToPseudoIndexMapping.Invoke(raw, rawToPseudoIndexMappingArg);
        return pseudoIndexNormalizer is null ? v1 : pseudoIndexNormalizer.Invoke(v1);
    }
}