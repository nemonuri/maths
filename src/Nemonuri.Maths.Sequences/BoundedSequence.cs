namespace Nemonuri.Maths.Sequences;

public class BoundedSequence<T> : IReadOnlyList<T>
{
    public IBoundableSequencePremise<T> Premise {get;}
    
    public ITolerantInterval<T> TolerantInterval {get;}

    public T ZeroIndex {get;}
    
    public int Count {get;}

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
        int count = 
            premise.GetCount
            (
                tolerantInterval.LeftMain.Anchor,
                tolerantInterval.LeftMain.ClosedDirection,
                tolerantInterval.RightMain.Anchor,
                tolerantInterval.RightMain.ClosedDirection
            );
        
        //if (leftToleranceAlternativeIndexFactory.)
        //---|
    }

    public T this[int index] => Premise.GetItem(index);
   

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly BoundedSequence<T> _innerSource;

        private T _current;

        public Enumerator(BoundedSequence<T> innerSource)
        {
            _innerSource = innerSource;
            _current = _innerSource.ZeroIndex;
        }

        public T Current => _current;
        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_innerSource.Premise.TryGetSuccessor(_current, out T? outSuccessor))
            {
                _current = outSuccessor;
            }
            
            return false;
        }

        public void Reset()
        {
            _current = _innerSource.ZeroIndex;
        }
    }
}
