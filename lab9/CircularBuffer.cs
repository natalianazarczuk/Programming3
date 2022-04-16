using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab09_EN
{
    //Stage 1 - 2.5 points
    //Inside CircularBuffer.cs file create CircularBuffer<T> generic class which implements interface IBuffer<T> (interface definition is in file Program.cs)
    //CircularBuffer<T> constructor takes as parameter number of maximum stored values in the buffer.
    //Method Put adds a new value to the buffer.If the buffer is full, throws an exception with message "Full buffer".
    //Method Get returns the oldest stored value in the buffer. If empty, throws an exception with message "Empty buffer".
    //Property Size returns the maximal number which can be stored in the buffer
    //Property Count returns the current number of stored values.
    //Properties Empty and Full returns respectively true if the buffer is empty and full.
    //Method Reset allows for resetting the state of buffer - clean stored values.
    class CircularBuffer<T> : IBuffer<T>, IEnumerable where T:IComparable<T>
    {
        private T[] buff;
        private uint max;
        private uint elements = 0;

        public CircularBuffer(uint m) {
            max = m;
            buff = new T[max];
            elements = 0;
        }

        public uint Size
        {
           get => max;
        }

        public uint Count
        {
            get => elements;
        }

        public bool Empty
        {
            get
            {
                return (elements==0);
            }
        }

        public bool Full
        {
            get
            {
                return (elements==max);
            }
        }


        public void Put(T value)
        {
            if (Full) 
            {
                Console.WriteLine("Full buffer");
                throw new Exception();
            }

            buff[elements] = value;
            elements++;
        }
        public T Get()
        {
            if (Empty)
            {
                Console.WriteLine("Empty buffer");
                throw new Exception();
            }

            T temp = buff[0];
            for(int i=0; i<elements-1; i++)
            {
                buff[i] = buff[i + 1];
            }
            elements--;
            return temp;
        }
      
        public void Reset()
        {
            elements = 0;
        }

        public IEnumerator GetEnumerator()
        {
            foreach(var i in buff)
            {
                yield return i;
            }
        }

        public IEnumerable FilterLowerThan(T value)
        {
            foreach(var i in buff)
            {
                if (i.CompareTo(value) < 0)
                    yield return i;
            }
        }
  
    }
}
