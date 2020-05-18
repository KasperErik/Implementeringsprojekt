using System;
using System.Numerics;

namespace Project
{
	class Program
	{
		static void Main()
		{
			Tests tests = new Tests(10000000, 25);
			tests.TestHashtable(20, HashFunctions.Instance.Multiply_shift_hashing);
			long a = tests.HashFunctionTest(HashFunctions.Instance.Multiply_shift_hashing);
			long b = tests.HashFunctionTest(HashFunctions.Instance.Multiply_mod_prime_hashing);
			Console.WriteLine(a);
			Console.WriteLine(b);
			float a1 = float.Parse(a.ToString());
			float b1 = float.Parse(b.ToString());
			Console.WriteLine(b1 / a1);
		}
	}
}
