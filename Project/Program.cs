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
			float a1 = float.Parse(a.ToString());
			float b1 = float.Parse(b.ToString());
			Console.WriteLine(b1 / a1);
			*/
			for (int j = 0; j < 100; j++)
			{
				for (int i = 1; i < 10; i++)
				{
					IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(1000000, i);
					ulong sum = tests.TestSquaredSum(stream, HashFunctions.Instance.Multiply_shift_hashing, i);
					Console.WriteLine(sum);
				}
			}
		}
	}
}
