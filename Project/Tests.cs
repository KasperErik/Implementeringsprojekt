using System;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project
{
	class Tests
	{
		readonly IEnumerable<Tuple<ulong, int>> stream;
		readonly List<ulong> values = new List<ulong>();
		readonly Stopwatch stopwatch = new Stopwatch();
		long time;
		BigInteger sum = 0;
		public Tests(int n, int l)
		{
			stream = Stream.CreateStream(n, l);

			foreach (var i in stream)
			{
				values.Add(i.Item1);
			}
		}
		public long HashFunctionTest(Func<ulong, ulong> f)
		{
			List<ulong> hashValues = new List<ulong>();

			stopwatch.Start();
			foreach (var i in values)
			{
				hashValues.Add(f(i));
			}
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();

			for (int i = 0; i < hashValues.Count; i++)
			{
				sum += hashValues[i];
			}
			Debug.WriteLine(sum.ToString("N0"));
			return time;
		}
		public void TestHashtable(int size, Func<ulong, ulong> function)
		{
			FixedSizeGenericHashTable hash = new FixedSizeGenericHashTable(function, size);

			Console.WriteLine(hash.Get(1).ToString());
			hash.Set(1, 10);
			Console.WriteLine(hash.Get(1).ToString());
			hash.Set(1, 20);
			Console.WriteLine(hash.Get(1).ToString());
			hash.Increment(1, 20);
			Console.WriteLine(hash.Get(1).ToString());
			hash.Remove(1);
			Console.WriteLine(hash.Get(1).ToString());
		}
		public ulong TestSquaredSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, ulong> f, int size)
        {
			SquaredSum fancy = new SquaredSum();
			ulong sum = fancy.SquareSum(stream, f, size);
            return sum;
        }
	}
}
