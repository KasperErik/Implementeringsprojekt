using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class HashFunctions
	{
		public BigInteger Multiply_shift_hashing(BigInteger x)
		{
			Random rnd = new Random();
			BigInteger a = BigInteger.Parse("11868245815727406823"); //1010010010110100011110101011000001010001011100101011111011100111 11868245815727406823
			int l = rnd.Next(0, 63);
			return (a * x) >> (64 - l);
		}
		public BigInteger Multiply_mod_prime_hashing(BigInteger x)
		{
			Random rnd = new Random();
			BigInteger p = (1 << 88) - 1;   //  011111111111111111111 want to do left shift while maintaining 
			byte[] bytes = p.ToByteArray();
			rnd.NextBytes(bytes);
			bytes[bytes.Length - 1] &= (byte)0x7F;
			BigInteger b = new BigInteger(bytes);
			BigInteger a = BigInteger.Parse("11868245815727406823"); //1010010010110100011110101011000001010001011100101011111011100111 11868245815727406823
			int l = rnd.Next(0, 63);
			//Use ex 2.7 2.8
			BigInteger y = (a * x) + b;
			if (y >= p)
			{
				y -= p; //y mod p = y - p , if y >= p
			}
			BigInteger result = y - ((y >> l) << l); //ex 2.7 y mod 2^l
			return result;
		}
	}
}
