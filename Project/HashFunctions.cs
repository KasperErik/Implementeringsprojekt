using System.Numerics;
using System.Collections.Generic;
using System;

namespace Project
{
	public class HashFunctions
	{
		private readonly ulong a1;
		private readonly BigInteger a2;
		//These are used for 4 universal hashfunktion
		private List<BigInteger> a_values = new List<BigInteger>();
		private readonly BigInteger b;
		private HashFunctions()
		{
			a1 = ulong.Parse("11868245815727406823");
			a2 = BigInteger.Parse("749575721744620932214764");
			b = BigInteger.Parse("733936089202998014171360"); // Get another number
															  //These are used for 4 universal hashfunktion
			a_values.Add(BigInteger.Parse("365300058338701456310658672"));
			a_values.Add(BigInteger.Parse("170518757745832412114201869"));
			a_values.Add(BigInteger.Parse("292558019870288895792685185"));
			a_values.Add(BigInteger.Parse("560243575461240827174015506"));
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

		public ulong Multiply_shift_hashing(ulong x, int l)
		{
			return (a1 * x) >> (64 - l);
		}
		public ulong Multiply_mod_prime_hashing(ulong x, int l)
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

		public ulong Four_universel_hashfunktion(ulong x, int l)
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
		public Tuple<BigInteger, BigInteger> Hash_func_for_count(ulong hashoutput, int l)
		{
			BigInteger h = hashoutput & (ulong)(l - 1);
			BigInteger b = hashoutput >> (89 - 1);
			BigInteger s = 1 - 2 * b;
			return Tuple.Create(s, h);
		}
	}
}
