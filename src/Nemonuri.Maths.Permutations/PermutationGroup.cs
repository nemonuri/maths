namespace Nemonuri.Maths.Permutations;

public readonly ref struct PermutationGroup
{
    private readonly ReadOnlySpan<int> _innerPermutationGroup;

    public PermutationGroup(ReadOnlySpan<int> innerPermutationGroup)
    {
        Guard.IsTrue(PermutationTheory.IsNormalizedPermutationGroup(innerPermutationGroup));
        _innerPermutationGroup = innerPermutationGroup;
    }

    public ReadOnlySpan<int> InnerPermutationGroup => _innerPermutationGroup;

    public int Length => _innerPermutationGroup.Length;

    public bool IsEmpty => _innerPermutationGroup.IsEmpty;

    public PermutationGroup GetInversePermutationGroup(Span<int> destination)
    {
        Guard.IsGreaterThanOrEqualTo(destination.Length, Length);

        Span<int> slicedDestination = destination[..Length];

        PermutationTheory.GetInverseNormalizedPermutationGroup
        (
            _innerPermutationGroup,
            slicedDestination,
            guardingSourceIsNormalizedPermutationGroup: false
        );

        return new PermutationGroup(slicedDestination);
    }

    public bool TryGetInversePermutationGroup(Span<int> destination, out PermutationGroup permutationGroup)
    {
        if (!IsEmpty)
        {
            permutationGroup = default;
            return false;
        }

        permutationGroup = GetInversePermutationGroup(destination);
        return true;
    }

    public void Apply<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : unmanaged
    {
        PermutationTheory.ApplyMultiProjection
        (
            source,
            _innerPermutationGroup,
            destination
        );
    }

    public bool TryApply<T>(ReadOnlySpan<T> source, Span<T> destination)
        where T : unmanaged
    {
        if (IsEmpty) 
        {
            return false;
        }

        Apply(source, destination);
        return true;
    }

    public void Apply<T>(ReadOnlySpan<T> source, Span<T> destination, Span<T> intermediate)
    {
        PermutationTheory.ApplyMultiProjection
        (
            source,
            intermediate,
            _innerPermutationGroup,
            destination
        );
    }

    public bool TryApply<T>(ReadOnlySpan<T> source, Span<T> destination, Span<T> intermediate)
    {
        if (IsEmpty) 
        {
            return false;
        }

        Apply(source, destination, intermediate);
        return true;
    }
}