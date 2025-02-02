namespace Nemonuri.Maths;

[Flags]
public enum CompareConditions
{
    None = 0,
    Less = 1 << 0,
    Equal = 1 << 1,
    Greater = 1 << 2,
    LessOrEqual = Less | Equal,
    GreaterOrEqual = Greater | Equal,
    All = Less | Equal | Greater
}
