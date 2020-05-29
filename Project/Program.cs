using System;
using System.Collections.Generic;

namespace Project
{
	internal class Program
	{
		private static void Main()
		{
			Tests tests = new Tests(10000000, 25);
			/*
			tests.TestHashtable(20, HashFuncts.Instance.MultiShift);
			long a = tests.HashFunctionTest(HashFuncts.Instance.MultiShift);
			long b = tests.HashFunctionTest(HashFuncts.Instance.MultiModPrime);
			Console.WriteLine(a);
			Console.WriteLine(b);
			float a1 = (float)a;
			float b1 = (float)b;
			Console.WriteLine(b1 / a1);
			*/

			/*
			Opgave 3
			*/
			for (int i = 1; i < 30; i++)
			{
				Console.WriteLine("\n-----------------------------------");
				Console.WriteLine("Run with l = " + i.ToString());
				Console.WriteLine("-----------------------------------\n");

				IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(1000000, i);
				long res1 = tests.TestSqredSum(stream, HashFuncts.MultiShift, i);

				Console.WriteLine("Shift time: " + res1.ToString() + "\n");

				stream = Stream.CreateStream(1000000, i);
				long res2 = tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

				Console.WriteLine("Mod p time: " + res2.ToString());

				float res = (float)res2 / (float)res1;
				Console.WriteLine("\nDifference: " + res.ToString());
			}
		}
	}
}