namespace Nemonuri.Maths.Sequences;

public readonly struct SudoIndexToIntegerMappingUnion<TNumber>
{
    public SudoIndexToIntegerMappingUnion(SudoIndexToIntegerMappingKind mappingKind, SudoIndexToIntegerMapper<TNumber>? customMapper)
    {
        MappingKind = mappingKind;
        CustomMapper = customMapper;
    }

    public SudoIndexToIntegerMappingKind MappingKind {get;}
    public SudoIndexToIntegerMapper<TNumber>? CustomMapper {get;}
}
