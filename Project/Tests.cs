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
		public void TestHashtable(int size, Func<ulong, int, ulong> function)
		{
			FixedSizeGenericHashTable hash = new FixedSizeGenericHashTable(function, size);

			Console.WriteLine(hash.Get(1).ToString());
			hash.Set(1, 10);
			Console.WriteLine(hash.Get(1).ToString());
			hash.Set(1, 20);
			Console.WriteLine(hash.Get(1).ToString());
			hash.Increment(1, 20);
			Console.WriteLine(hash.Get(1).ToString());
		}
		public long TestSquaredSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			SquaredSum fancy = new SquaredSum();
			stopwatch.Start();
			ulong sum = fancy.SquareSum(stream, f, size);
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			Debug.WriteLine(sum);
			return time;
		}
		public long TestCount(IEnumerable<Tuple<ulong, int>> stream, int l)
		{
			HashFunctions f = HashFunctions.Instance;
			foreach (x in stream)
			{
				ulong[] something = Count_Sketch.Sketch(f.Four_universel_hashfunktion(x, l), f.Hash_func_for_count(f.Four_universel_hashfunktion(x, l), l), stream, l);

			}
			return default;
		}
	}
}
