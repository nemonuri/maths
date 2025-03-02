namespace Nemonuri.Maths.Permutations;

public static class PermutationTheory
{
    public static void ApplyNormalizedPermutationGroup<T>
    (
        ReadOnlySpan<T> source,
        ReadOnlySpan<int> normalizedPermutationGroup,
        Span<T> destination,
        Span<int> inverseNormalizedPermutationGroupDestination = default,
        bool guardingNormalizedPermutationGroupIsValid = true
    )
        where T : unmanaged
    {
        //--- Guard ---
        if (guardingNormalizedPermutationGroupIsValid)
        {
            Guard.IsEqualTo(source.Length, normalizedPermutationGroup.Length);
            Guard.IsTrue(IsNormalizedPermutationGroup(normalizedPermutationGroup));
        }

        if (!inverseNormalizedPermutationGroupDestination.IsEmpty)
        {
            Guard.IsEqualTo(normalizedPermutationGroup.Length, inverseNormalizedPermutationGroupDestination.Length);
        }
        //---|

        ApplyMultiProjection
        (
            source,
            normalizedPermutationGroup,
            destination
        );

        GetInverseNormalizedPermutationGroup
        (
            normalizedPermutationGroup,
            inverseNormalizedPermutationGroupDestination,
            false
        );
    }

    public static void ApplyMultiProjection<T>
    (
        ReadOnlySpan<T> source,
        ReadOnlySpan<int> projectionIndexes,
        Span<T> destination
    )
        where T : unmanaged
    {
        ApplyMultiProjection
        (
            source,
            stackalloc T[source.Length],
            projectionIndexes,
            destination
        );
    }

    public static void ApplyMultiProjection<T>
    (
        ReadOnlySpan<T> source,
        Span<T> copiedSourceBuffer,
        ReadOnlySpan<int> projectionIndexes,
        Span<T> destination
    )
    {
        Guard.IsEqualTo(source.Length, copiedSourceBuffer.Length);
        Guard.IsEqualTo(projectionIndexes.Length, destination.Length);

        source.CopyTo(copiedSourceBuffer);

        for (int i = 0; i < projectionIndexes.Length; i++)
        {
            int projectionIndex = projectionIndexes[i];
            destination[i] = copiedSourceBuffer[projectionIndex];
        }
    }

    public static void GetInverseNormalizedPermutationGroup
    (
        ReadOnlySpan<int> source,
        Span<int> destination,
        bool guardingSourceIsNormalizedPermutationGroup = true
    )
    {
        Guard.IsEqualTo(source.Length, destination.Length);

        if (guardingSourceIsNormalizedPermutationGroup)
        {
            Guard.IsTrue(IsNormalizedPermutationGroup(source));
        }

        //--- Create Cauchy's two-line notation ---
        /**
        Reference:
        - https://en.wikipedia.org/wiki/Permutation_group#Neutral_element_and_inverses
        */
        Span<int> cauchyFirstLine = stackalloc int[source.Length];
        source.CopyTo(cauchyFirstLine);

        Span<int> cauchySecondLine = stackalloc int[source.Length];
        for (int i = 0; i < cauchySecondLine.Length; i++)
        {
            cauchySecondLine[i] = i;
        }
        //---|

        //--- Sort Cauchy two line ---
        cauchyFirstLine.Sort(cauchySecondLine);
        //---|
        
        cauchySecondLine.CopyTo(destination);
    }

    public static bool IsNormalizedPermutationGroup
    (
        ReadOnlySpan<int> permutationGroup
    )
    {
        /*
        Examples
        - True: [0,1,2,3], [4,2,5,1,3,0]
        - False: [0,1,1,3], [4,2,5,1,3]
        */

        int length = permutationGroup.Length;

        //--- Create bitfield filled with 1 (using two's complement) ---
        //ex) ...1111111111
        int bitField = -1;
        //---|

        //--- Left shift ---
        //ex) ...1111110000 (if length is 4)
        bitField <<= length;
        //---|

        //--- Fill bitField using |(logical or) ---
        for (int i = 0; i < length; i++)
        {
            int v1 = 1 << i;
            bitField |= v1;
        }
        //---|

        //--- If bitField is -1, return true ---
        return bitField == -1;
        //---|
    }


}
