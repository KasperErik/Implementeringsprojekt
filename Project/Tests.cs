using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Xml;

namespace Project
{
	internal class Tests
	{
		private readonly IEnumerable<Tuple<ulong, int>> stream;
		private readonly List<ulong> values = new List<ulong>();
		private readonly Stopwatch stopwatch = new Stopwatch();
		private long time;
		private BigInteger sum = 0;

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

		public ulong TestSqredSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			stopwatch.Start();
			ulong sum = SquaredSum.SquareSum(stream, f, size);
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			Console.WriteLine("Time used: " + time);
			return sum;
		}

		public ulong TestCount(IEnumerable<Tuple<ulong, int>> stream, int l)
		{
			ulong[] arr = CountSketch.Sketch(HashFuncts.FourUniversal, HashFuncts.Hash4Count, stream, l);
			ulong result = CountSketch.Estimate(arr);
			return result;
		}
	}
}