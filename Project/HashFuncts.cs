using System;
using System.Numerics;

namespace Project
{
	public class HashFuncts
	{
		private static readonly ulong a1 = ulong.Parse("11868245815727406823");
		private static readonly BigInteger a2 = BigInteger.Parse("749575721744620932214764");
		private static readonly BigInteger b = BigInteger.Parse("733936089202998014171360");

		private static readonly BigInteger[] a_values = new BigInteger[]
		{
			BigInteger.Parse("365300058338701456310658672"),
			BigInteger.Parse("170518757745832412114201869"),
			BigInteger.Parse("292558019870288895792685185"),
			BigInteger.Parse("560243575461240827174015506")
		};

		public static ulong MultiShift(ulong x, int l)
		{
			return (a1 * x) >> (64 - l);
		}

		public static ulong MultiModPrime(ulong x, int l)
		{
			BigInteger p = (1 << 89) - 1;   //  011111111111111111111 want to do left shift while maintaining
											//Use ex 2.7 2.8
			BigInteger y = (a2 * x) + b; //(a · x + b)
			y = (y & p) + (y >> 89); // what is q?
			if (y >= p)
			{
				y -= p; //y mod p = y - p , if y >= p
			}
			return (ulong)(y - ((y >> l) << l));
		}

		public static ulong FourUniversal(ulong x, int l)
		{
			//q is 89
			BigInteger p = (1 << 89) - 1;
			//This should be a_q-1
			BigInteger y = a_values[3];
			//Run through the list of a values
			for (int i = 2; i >= 0; i--)
			{
				y = y * x + a_values[i];
				y = (y & p) + (y >> 89); //89 is b in the algorithm
			}
			if (y >= p)
			{
				y -= p;
			}
			// l most significant bits
			return (ulong)(y - (y >> l) << l);
		}

		public static Tuple<ulong, ulong> Hash4Count(ulong hashoutput, int l)
		{
			ulong h = hashoutput & (ulong)(l - 1);
			ulong b = hashoutput >> (l - 1);
			ulong s = 1 - 2 * b;
			return Tuple.Create(s, h);
		}
	}
}