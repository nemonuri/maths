namespace Nemonuri.Maths;

public static class MappingPremiseTheory
{
    public static TCodomain Map<TDomain, TCodomain>
    (
        this IMappingPremise<TDomain, TCodomain> premise,
        TDomain item
    )
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TCodomain : allows ref struct
#endif
    {
        Guard.IsNotNull(premise);

        if (premise.TryMap(item, out TCodomain? outResult))
        {
            return outResult;
        }

        throw new InvalidOperationException(/* TODO */);
    }

    public static TDomain InverseMap<TDomain, TCodomain>
    (
        this IInvertibleMappingPremise<TDomain, TCodomain> premise,
        TCodomain item
    )
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TCodomain : allows ref struct
#endif
    {
        Guard.IsNotNull(premise);

        if (premise.TryInverseMap(item, out TDomain? outResult))
        {
            return outResult;
        }

        throw new InvalidOperationException(/* TODO */);
    }
}