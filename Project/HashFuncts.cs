﻿using System;
using System.Numerics;

namespace Project
{
	public class HashFuncts
	{
		private static readonly ulong a1 = ulong.Parse("11868245815727406823");
		private static readonly BigInteger a2 = BigInteger.Parse("749575721744620932214764");
		private static readonly BigInteger b = BigInteger.Parse("733936089202998014171360");

		private static BigInteger[] a_values = new BigInteger[]
		{
			BigInteger.Parse("365300058338701456310658672"),
			BigInteger.Parse("170518757745832412114201869"),
			BigInteger.Parse("292558019870288895792685185"),
			BigInteger.Parse("560243575461240827174015506")
		};

		//can take arguement int32

		public static void PrintaValues()
		{
			Console.WriteLine("a0 : {0:N0}\n a1 : {1:N0}\n a2 : {2:N0}\n a3 : {3:N0}\n\n", a_values[0], a_values[1], a_values[2], a_values[3]);
		}


		public static void ChangeRandArray(RandGen randGen)
		{
			//1 << 89 - 1
			a_values[0] = randGen.randomBigInteger(((BigInteger)1 << 89) - 2);
			a_values[1] = randGen.randomBigInteger(((BigInteger)1 << 89) - 2);
			a_values[2] = randGen.randomBigInteger(((BigInteger)1 << 89) - 2);
			a_values[3] = randGen.randomBigInteger(((BigInteger)1 << 89) - 2);
		}

		public static ulong MultiShift(ulong x, int l)
		{
			return (a1 * x) >> (64 - l);
		}

		public static ulong MultiModPrime(ulong x, int l)
		{
			BigInteger p = ((BigInteger)1 << 89) - (BigInteger)1;
			BigInteger y = (a2 * x) + b;
			y = (y & p) + (y >> 89);
			if (y >= p)
			{
				y -= p;
			}
			return (ulong)(y - ((y >> l) << l));
		}

		public static BigInteger FourUniversal(ulong x)
		{
			BigInteger p = ((BigInteger)1 << 89) - (BigInteger)1;
			BigInteger y = a_values[3];

			for (int i = 2; i >= 0; i--)
			{
				y = (y * x) + a_values[i];
				y = (y & p) + (y >> 89); //89 is b in the algorithm
			}

			return y >= p ? (y -= p) : y;
		}

		public static Tuple<int, int> Hash4Count(BigInteger hashoutput, int m)
		{
			int h = (int)(hashoutput & (BigInteger)(m - 1));
			//89 is the power in p = 2**89 - 1
			// b is either 0 or 1, its the first bit
			int b = (int)(hashoutput >> (89 - 1));
			int s = 1 - (2 * b);
			return Tuple.Create(s, h);
		}
	}
}