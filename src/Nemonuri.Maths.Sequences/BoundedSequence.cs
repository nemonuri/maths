namespace Nemonuri.Maths.Sequences;

public class BoundedSequence<T> : IReadOnlyList<T>
    where T : IComparable<T>
{
    public ISequencePremise<T> SequencePremise {get;}
    public ISequenceBoundingPremise<T> SequenceBoundingPremise {get;}
    public ISequenceItemToIndexPremise<T> SequenceItemToIndexPremise {get;}
    
    public ITolerantInterval<T> TolerantInterval {get;}

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
        ISequencePremise<T> sequencePremise,
        ISequenceBoundingPremise<T> sequenceBoundingPremise,
        ISequenceItemToIndexPremise<T> sequenceItemToIndexPremise,
        ITolerantInterval<T> tolerantInterval,
        IAlternativeIndexFactory<T, object?, int>? leftToleranceAlternativeIndexFactory,
        IAlternativeIndexFactory<T, object?, int>? rightToleranceAlternativeIndexFactory
    )
    {
        Guard.IsNotNull(sequencePremise);
        Guard.IsNotNull(sequenceBoundingPremise);
        Guard.IsNotNull(sequenceItemToIndexPremise);
        Guard.IsNotNull(tolerantInterval);

        SequencePremise = sequencePremise;
        SequenceBoundingPremise = sequenceBoundingPremise;
        SequenceItemToIndexPremise = sequenceItemToIndexPremise;
        TolerantInterval = tolerantInterval;

        //--- Count 구하기 ---
        Count = 
            sequenceBoundingPremise.GetCount
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
            leftToleranceAlternativeIndexFactory?.TryGetAlternativeIndex<T, int, object, object>
            (
                tolerantInterval.LeftMain.Anchor,
                null,
                out int v1
            ) ?? 
            false
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
            rightToleranceAlternativeIndexFactory?.TryGetAlternativeIndex<T, int, object, object>
            (
                tolerantInterval.RightMain.Anchor,
                null,
                out int v2
            ) ?? 
            false
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

    public T this[int index] => SequencePremise.GetItem(index);

    public int GetIndex(T item) => SequenceItemToIndexPremise.GetIndex(item);
   

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly BoundedSequence<T> _innerSource;

        private bool _inited;
        private int _currentIndex;

        public Enumerator(BoundedSequence<T> innerSource)
        {
            _innerSource = innerSource;
            Reset();
        }

        public T Current => _innerSource[_currentIndex];
        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!_inited) 
            {
                _inited = true;
                return true;
            }

            int nextIndex = _currentIndex + 1;
            if
            (
                (_innerSource.LeastIndex <= nextIndex) &&
                (nextIndex <= _innerSource.GreatestIndex)
            )
            {
                _currentIndex = nextIndex;
                return true;
            }
            
            return false;
        }

        public void Reset()
        {
            _inited = false;
            _currentIndex = _innerSource.LeastIndex;
        }
    }
}
