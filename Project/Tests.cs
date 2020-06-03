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
		public static bool verbatim = true;


		public static long HashFunctionTest(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int l)
		{
			ulong sum = 0;
			stopwatch.Start();
			foreach (Tuple<ulong, int> item in stream)
			{
				sum += f(item.Item1, l);
			}
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			Debug.WriteLine(sum.ToString("N0"));
			return time;
		}

		public static long HashFunctionTest(IEnumerable<Tuple<ulong, int>> stream, Func<ulong,  BigInteger> f)
		{
			BigInteger sum = 0;
			stopwatch.Start();
			foreach (Tuple<ulong, int> item in stream)
			{
				sum += f(item.Item1);
			}
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			Debug.WriteLine(sum.ToString("N0"));
			return time;
		}

		public static (ulong sum, long time) TestSqredSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			stopwatch.Start();
			ChainedHashTable table = SquaredSum.FillHashtable(stream, f, size);
			ulong sum = SquaredSum.SquareSum(table, size);
			stopwatch.Stop();
			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			return (sum, time);
		}

		public static (ulong[] arr, long time) TestCount(IEnumerable<Tuple<ulong, int>> stream, int m)
		{
			ulong[] res = new ulong[100];
			for (int j = 0; j < 100; j++)
			{
				//Countsketch
				HashFuncts.ChangeRandArray(randGen);
				stopwatch.Start();
				int[] arr = CountSketch.Sketch(HashFuncts.FourUniversal, HashFuncts.Hash4Count, stream, m);
				res[j] = CountSketch.Estimate(arr);
				stopwatch.Stop();
				if (verbatim)
				{
					string str = String.Format("Count Sketch Estimate : {0,10}", res[j]);
					PrettyPrint.Body(str);
				}
				else
				{
					string str = $"Count Sketch Estimates: m = {m}";
					PrettyPrint.ProgressBar(str, j + 1, 100);
				}
			}

			time = stopwatch.ElapsedMilliseconds;
			stopwatch.Reset();
			return (res, time);
		}
	}
}