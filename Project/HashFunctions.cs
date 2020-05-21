using System;
using System.Numerics;

namespace Project
{
	public class HashFunctions
	{
		private readonly ulong a1;
		private readonly BigInteger a2;
		private readonly BigInteger b;
		private readonly int l;
		private HashFunctions()
		{
			Random rnd = new Random();
			a1 = ulong.Parse("11868245815727406823");
			a2 = BigInteger.Parse("749575721744620932214764");
			b = BigInteger.Parse("733936089202998014171360"); // Get another number
			l = rnd.Next(1, 64);
		}

		private static HashFunctions instance = null;
		public static HashFunctions Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new HashFunctions();
				}
				return instance;
			}
		}

		public ulong Multiply_shift_hashing(ulong x)
		{
			return (a1 * x) >> (64 - l);
		}
		public ulong Multiply_mod_prime_hashing(ulong x)
		{
			BigInteger p = (1 << 89) - 1;   //  011111111111111111111 want to do left shift while maintaining 
											//Use ex 2.7 2.8
			BigInteger y = (a2 * x) + b; //(a · x + b)
			y = (y & p) + (y >> 89); // what is q?
			if (y >= p)
			{
				y -= p; //y mod p = y - p , if y >= p
			}
			BigInteger result = y - ((y >> l) << l); //testing 1mil values, it stayed under ulong
			return (ulong)result;
		}
	}
}
