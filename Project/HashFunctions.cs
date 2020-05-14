using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class HashFunctions
	{
		private BigInteger a1;
		private BigInteger a2;
		private BigInteger b;
		private int l;
		public HashFunctions()
		{
			Random rnd = new Random();
			a1 = BigInteger.Parse("11868245815727406823");
			a2 = BigInteger.Parse("749575721744620932214764");
			b = BigInteger.Parse("733936089202998014171360"); // Get another number
			l = rnd.Next(1, 64);
		}
		public BigInteger Multiply_shift_hashing(BigInteger x)
		{
			return (a1 * x) >> (64 - l);
		}
		public BigInteger Multiply_mod_prime_hashing(BigInteger x)
		{
			BigInteger p = (1 << 88) - 1;   //  011111111111111111111 want to do left shift while maintaining 
			//Use ex 2.7 2.8
			BigInteger y = (a2 * x) + b;
			if (y >= p)
			{
				y -= p; //y mod p = y - p , if y >= p
			}
			BigInteger result = y - ((y >> l) << l); //ex 2.7 y mod 2^l
			return result;
		}
	}
}
