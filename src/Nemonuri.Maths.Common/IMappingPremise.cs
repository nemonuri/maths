namespace Nemonuri.Maths;

public interface IMappingPremise<TDomain, TCodomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TCodomain : allows ref struct
#endif
{
    bool TryMap(TDomain item, [NotNullWhen(true)] out TCodomain? outResult);
}

public interface IInvertibleMappingPremise<TDomain, TCodomain> : IMappingPremise<TDomain, TCodomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TCodomain : allows ref struct
#endif
{
    bool TryInverseMap(TCodomain item, [NotNullWhen(true)] out TDomain? outResult);
}
