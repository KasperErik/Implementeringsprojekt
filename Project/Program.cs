using System;
using System.Numerics;
using System.Collections.Generic;

namespace Project
{
    class Program
    {
        static void Main()
        {
            int n = 10;
            int l = 5;

            IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);

            HashFunctions functions = new HashFunctions();

            List<BigInteger> add_list = new List<BigInteger>();
            foreach (var i in stream)
            {
                var value = functions.Multiply_shift_hashing(i.Item1);
                add_list.Add(value);
            }

            for (int i = 0; i < add_list.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(add_list[i].ToString("N0"));
            }
        }
    }
}
