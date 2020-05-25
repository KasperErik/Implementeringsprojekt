using System;
using System.Collections.Generic;
using System.Numerics;

namespace Project
{
	class Program
	{
		static void Main()
		{
			Tests tests = new Tests(10000000, 25);
			/*
			tests.TestHashtable(20, HashFunctions.Instance.Multiply_shift_hashing);
			long a = tests.HashFunctionTest(HashFunctions.Instance.Multiply_shift_hashing);
			long b = tests.HashFunctionTest(HashFunctions.Instance.Multiply_mod_prime_hashing);
			Console.WriteLine(a);
			Console.WriteLine(b);
			float a1 = (float)a;
			float b1 = (float)b;
			Console.WriteLine(b1 / a1);
			*/
			HashFunctions f = HashFunctions.Instance;
			for (int i = 1; i < 30; i++)
			{
				Console.WriteLine("\n-----------------------------------");
				Console.WriteLine("Run with l = " + i.ToString());
				Console.WriteLine("-----------------------------------\n");

				IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(1000000, i);
				long res1 = tests.TestSquaredSum(stream, f.Multiply_shift_hashing, i);
				Console.WriteLine("Shift time: " + res1.ToString());

				stream = Stream.CreateStream(1000000, i);
				long res2 = tests.TestSquaredSum(stream, f.Multiply_mod_prime_hashing, i);
				Console.WriteLine("Mod p time: " + res2.ToString());

				float res = (float)res2 / (float)res1;
				Console.WriteLine("\nDifference: " + res.ToString());
			}
		}
	}
}
