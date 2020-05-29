using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Project
{
	internal class Program
	{
		private static void Main()
		{
			Tests tests = new Tests(10000000, 25);
			/*
			tests.TestHashtable(20, HashFuncts.Instance.MultiShift);
			long a = tests.HashFunctionTest(HashFuncts.Instance.MultiShift);
			long b = tests.HashFunctionTest(HashFuncts.Instance.MultiModPrime);
			Console.WriteLine(a);
			Console.WriteLine(b);
			float a1 = (float)a;
			float b1 = (float)b;
			Console.WriteLine(b1 / a1);
			*/

			/*
			Opgave 3
			*/
			ulong[] Xarr = new ulong[100];
			int i = 15;
			IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(1000000, i);
			for (int j = 0; j < 100; j++)
			{

				/*
				Console.WriteLine("\n-----------------------------------");
				Console.WriteLine("Run with l = " + i.ToString());
				Console.WriteLine("-----------------------------------\n");

				//Hashtabke
				IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(1000000, i);
				long res1 = tests.TestSqredSum(stream, HashFuncts.MultiShift, i);

				Console.WriteLine("Shift  sum: " + res1.ToString() + "\n");

				stream = Stream.CreateStream(1000000, i);
				long res2 = tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

				Console.WriteLine("Mod p  sum: " + res2.ToString());

				float res = (float)res2 / (float)res1;
				Console.WriteLine("\nDifference: " + res.ToString());

				Console.WriteLine("\n-----------------------------------\n");
				*/
				//Countsketch

				ulong res3 = tests.TestCount(stream, i);
				//Modprime S

				Console.WriteLine("Count Sketch Squared Sum Estimate : " + res3.ToString() + "\n");
				Xarr[j] = res3;
				//float dif = (float)res1 - (float)res3;
				//Console.WriteLine("\nDifference: " + dif.ToString() + "\n");

			}
			//Our S value from our MultiModPrime
			ulong S = tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

			//Mean Square Error
			float MSE = CountSketch.MSE(Xarr, S);
			Console.WriteLine("MSE : " + MSE.ToString() + "\n");
			
			List<ulong[]> List = CountSketch.DivideIntoSubArr(Xarr);

			float[] MedianArr = new float[9];

			//calculate median for all the Arrays
			int count = 0;
			foreach (ulong[] arr in List)
			{
				Array.Sort(arr);
				//Find median in Arr by selection algorithm
				//Assign value to MedianArr
				float temp = (float)arr[(10 - 1) / 2] + (float)arr[10 / 2];
				MedianArr[count] = temp / 2.0f;
				count++;
			}

			Array.Sort(MedianArr);

			foreach (var item in MedianArr)
			{
				Console.WriteLine("MedianArr : " + item.ToString());
			}

		}
	}
}