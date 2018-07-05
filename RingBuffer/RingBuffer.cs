using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingBuffer
{
    public class RingBuffer<T>
    {
        private T[] _buf;
        private int _head;
        private int _tail;
        private readonly int _capacity;

        public RingBuffer(int capacity = 0x100)
        {
            _capacity = capacity;
            _buf = new T[_capacity];
            _head = 0;
            _tail = 0;
        }

        public void Put(T t)
        {
            // we're allowed to "overrun" the buffer, so no checks here
            _buf[_head] = t;
            _head = (_head + 1) % _capacity;
        }

        public T GetOne()
        {
            if (!Empty)
            {
                T res = _buf[_tail];
                _tail = (_tail + 1) % _capacity;
                return res;
            }
            return default(T);
        }

        public T[] Get()
        {
            if (!Empty)
            {
                T[] res = new T[Size];

                for (int i = 0; i < res.Length; ++i)
                {
                    res[i] = _buf[_tail];
                    _tail = (_tail + 1) % _capacity;
                }

                return res;
            }

            return new T[0];
        }

        public int Capacity
        {
            get => _capacity;
        }

        public int Size
        {
            get
            {
                if (_tail > _head)
                {
                    return (_capacity - _tail) + _head;
                }
                else
                {
                    return _head - _tail;
                }
            }
        }

        public bool Empty
        {
            get => _head == _tail;
        }

        public override string ToString()
        {
            return String.Join(",", _buf);
        }
    }
}
