using System.Collections;

namespace Nemonuri.Maths.Sequences;

public class Sequence<T> : IReadOnlyList<T>
{
    public T First {get;}
    public ISequencePremise<T> Premise {get;}

    public Sequence(T first, ISequencePremise<T> premise)
    {
        First = first;
        Premise = premise;
    }

    public T this[int index] => Premise.GetItem(index);

    public int Count => Premise.Count;

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly Sequence<T> _innerSource;

        private T _current;

        public Enumerator(Sequence<T> innerSource)
        {
            _innerSource = innerSource;
            _current = _innerSource.First;
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
            _current = _innerSource.First;
        }
    }
}
