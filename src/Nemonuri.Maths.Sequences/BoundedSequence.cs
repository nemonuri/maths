namespace Nemonuri.Maths.Sequences;

public class BoundedSequence<T> : IReadOnlyList<T>
        where T : IComparable<T>
{
    public IBoundableSequencePremise<T> Premise {get;}
    
    public ITolerantInterval<T> TolerantInterval {get;}

    public T ZeroIndex {get;}

    public int LeastIndex {get;}
    public int GreatestIndex {get;}
    public int Count {get;}

    public bool IsLeftTolerantIndexDefault {get;}
    public int AlternativeLeftTolerantIndex {get;}
    public int LeftTolerantIndex => IsLeftTolerantIndexDefault ? LeastIndex : AlternativeLeftTolerantIndex;

    public bool IsRightTolerantIndexDefault {get;}
    public int AlternativeRightTolerantIndex {get;}
    public int RightTolerantIndex => IsRightTolerantIndexDefault ? GreatestIndex : AlternativeRightTolerantIndex;

    public BoundedSequence
    (
        IBoundableSequencePremise<T> premise,
        ITolerantInterval<T> tolerantInterval,
        IExtraArgumentAttachedMapping<T, object?, int> rawToIndexMapping,
        IAlternativeIndexFactory<T, object?, int> leftToleranceAlternativeIndexFactory,
        IAlternativeIndexFactory<T, object?, int> rightToleranceAlternativeIndexFactory,
        T zeroIndex
    )
    {
        Guard.IsNotNull(premise);
        Guard.IsNotNull(tolerantInterval);
        Guard.IsNotNull(rawToIndexMapping);
        Guard.IsNotNull(leftToleranceAlternativeIndexFactory);
        Guard.IsNotNull(rightToleranceAlternativeIndexFactory);

        Premise = premise;
        TolerantInterval = tolerantInterval;
        ZeroIndex = zeroIndex;

        //--- Count 구하기 ---
        Count = 
            premise.GetCount
            (
                tolerantInterval.LeftMain.Anchor,
                tolerantInterval.LeftMain.ClosedDirection,
                tolerantInterval.RightMain.Anchor,
                tolerantInterval.RightMain.ClosedDirection,
                out int outLeastIndex,
                out int outGreatestIndex
            );
        
        LeastIndex = outLeastIndex;
        GreatestIndex = outGreatestIndex;

        if 
        (
            leftToleranceAlternativeIndexFactory.TryGetAlternativeIndex<T, int, object, object>
            (
                tolerantInterval.LeftMain.Anchor,
                null,
                out int v1
            )
        )
        {
            IsLeftTolerantIndexDefault = false;
            AlternativeLeftTolerantIndex = v1;
        }
        else
        {
            IsLeftTolerantIndexDefault = true;
            AlternativeLeftTolerantIndex = default;
        }

        if 
        (
            rightToleranceAlternativeIndexFactory.TryGetAlternativeIndex<T, int, object, object>
            (
                tolerantInterval.RightMain.Anchor,
                null,
                out int v2
            )
        )
        {
            IsRightTolerantIndexDefault = false;
            AlternativeRightTolerantIndex = v2;
        }
        else
        {
            IsRightTolerantIndexDefault = true;
            AlternativeRightTolerantIndex = default;
        }
    }

    public T this[int index] => Premise.GetItem(index);
   

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly BoundedSequence<T> _innerSource;

        private bool _inited;
        private T _current;

        public Enumerator(BoundedSequence<T> innerSource)
        {
            _innerSource = innerSource;
            Reset();
        }

        public T Current => _current;
        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!_inited) 
            {
                _inited = true;
            }

            if (_innerSource.Premise.TryGetSuccessor(_current, out T? outSuccessor))
            {
                _current = outSuccessor;
            }
            
            return false;
        }

        [MemberNotNull(nameof(_current))]
        public void Reset()
        {
            _inited = false;
            _current = _innerSource[_innerSource.LeastIndex];
        }
    }
}
