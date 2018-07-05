using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingBuffer
{
    class Program
    {
        static void TestFill<T>(RingBuffer<T> buf, params T[] data)
        {
            Console.WriteLine($"Inserting {data.Length} elements.");

            data.ToList().ForEach(x =>
                {
                    buf.Put(x);
                    Console.WriteLine($"Inserted {x}");
                }
            );

            Console.WriteLine("Buffer state: " + buf.ToString());
            Console.WriteLine($"Buffer size: {buf.Size}");

            Console.WriteLine("Reading full buffer.");

            T[] flush = buf.Get();
            Console.WriteLine("Returned: " + String.Join(",", flush));

            Console.WriteLine($"Is now empty? {buf.Empty}");
} 

        static void Main(string[] args)
        {
            var ringBuf = new RingBuffer<int>(8);

            TestFill(ringBuf, 1, 2, 3, 4, 5, 6, 7);

            TestFill(ringBuf, 0, 0, 0);

            Console.ReadKey();
        }
    }
}
