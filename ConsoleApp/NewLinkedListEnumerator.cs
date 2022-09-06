using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class NewLinkedListEnumerator<T> : IEnumerator<T>
    {
        private bool disposedValue;
        private T _current;
        private int _index; 
        private NewLinkedList<T> _list;

        public NewLinkedListEnumerator(NewLinkedList<T> list)
        {
            _list = list;
            _index = -1;
            _current = default(T);
        }



        public T Current => _current;

        object IEnumerator.Current => _current;

        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++_index >= _list.Count)
            {
                return false;
            }
            else
            {
                _current = _list.GetValue(_index);
            }
            return true;
        }

        public void Reset()
        {
            _current = default(T);
            _index = -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue=true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
