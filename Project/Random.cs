using System;
using System.Linq;
using System.Numerics;

namespace Project
{
	public class RandGen
	{
		static Random rand = new Random();

		//long sampleSize;
		//BigInteger max;
		//int[] heights;

		public delegate BigInteger RandomBigIntegerDelegate(BigInteger max);
		//RandomBigIntegerDelegate randomMethod;

		//returns time in miliseconds required to generate numbers
		//private int generateNumbers(long testCount, int width, BigInteger max)
		//{
		//	int begin = System.Environment.TickCount;
		//	this.heights = new int[width];

		//	for (long i = 0; i < testCount; i++)
		//	{
		//		BigInteger num = this.randomMethod(max);
		//		int bucket = (int)(num * width / max);
		//		heights[bucket]++;
		//	}
		//	int end = System.Environment.TickCount;
		//	return end - begin;
		//}

		// Generate a number from 1 to max (exclusive)
		private BigInteger randomBigIntegerMod(BigInteger max)
		{
			int length = (int)Math.Ceiling(BigInteger.Log(max, 2) / 31);
			BigInteger random_number = 0;
			for (int i = 0; i < length; i++)
				random_number = (random_number << 31) + rand.Next();
			return random_number % max;
		}

		static Random random = new Random();

		// returns a evenly distributed random BigInteger from 1 to N.
		public BigInteger randomBigInteger(BigInteger N)
		{
			BigInteger result = 0;
			do
			{
				int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
				int numBytes = (int)Math.Ceiling(length / 8.0);
				byte[] data = new byte[numBytes];
				random.NextBytes(data);
				result = new BigInteger(data);
			} while (result >= N || result < 0);
			return result;
		}
	}
}
