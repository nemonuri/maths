namespace Nemonuri.Maths.Sequences;

public readonly struct PseudoIndexNormalizingMethodUnion<TNumber>
{
    public PseudoIndexNormalizingMethodUnion(PseudoIndexNormalizingMethodKind mappingKind, PseudoIndexNormalizer<TNumber>? customMapper)
    {
        MappingKind = mappingKind;
        CustomMapper = customMapper;
    }

    public PseudoIndexNormalizingMethodKind MappingKind {get;}
    public PseudoIndexNormalizer<TNumber>? CustomMapper {get;}
}
