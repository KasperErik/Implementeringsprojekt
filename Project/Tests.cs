using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Project
{
	internal class Tests
	{
		private static readonly Stopwatch stopwatch = new Stopwatch();
		private static long time;
		private static readonly RandGen randGen = new RandGen();


		public static long HashFunctionTest(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int l)
		{
			List<ulong> hashValues = new List<ulong>();

			stopwatch.Start();

			foreach (Tuple<ulong, int> item in stream)
			{
				hashValues.Add(f(item.Item1, l));
			}

			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();

			ulong sum = 0;

			for (int i = 0; i < hashValues.Count; i++)
			{
				sum += hashValues[i];
			}

			Debug.WriteLine(sum.ToString("N0"));
			return time;
		}

		public static ulong TestSqredSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			stopwatch.Start();
			ulong sum = SquaredSum.SquareSum(stream, f, size);
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			Console.WriteLine(size + ", " + time);
			return sum;
		}

		public static ulong TestCount(IEnumerable<Tuple<ulong, int>> stream, int m)
		{
			HashFuncts.ChangeRandArray(randGen);
			ulong[] arr = CountSketch.Sketch(HashFuncts.FourUniversal, HashFuncts.Hash4Count, stream, m);
			ulong result = CountSketch.Estimate(arr);
			return result;
		}
	}
}