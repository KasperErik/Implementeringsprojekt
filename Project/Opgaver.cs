using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
	class Opgaver
	{
		public static void Opgave1(int n, int l)
		{
			IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);

			long a = Tests.HashFunctionTest(stream, HashFuncts.MultiShift, l);
			long b = Tests.HashFunctionTest(stream, HashFuncts.MultiModPrime, l);
			Console.WriteLine(a);
			Console.WriteLine(b);
			float a1 = (float)a;
			float b1 = (float)b;
			Console.WriteLine(b1 / a1);
		}

		public static void Opgave3(int n, int maxL)
		{
			IEnumerable<Tuple<ulong, int>> stream = default;

			for (int i = 1; i < maxL; i++)
			{
				Console.WriteLine("\n-----------------------------------");
				Console.WriteLine("Run with l = " + i.ToString());
				Console.WriteLine("-----------------------------------\n");

				stream = Stream.CreateStream(n, i);
				ulong res1 = Tests.TestSqredSum(stream, HashFuncts.MultiShift, i);

				Console.WriteLine("Shift sum: " + res1.ToString() + "\n");

				ulong res2 = Tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

				Console.WriteLine("Mod p sum: " + res2.ToString());
			}
		}

		public static void Opgave7(int n, int l, int m)
		{
			ulong[] Xarr = new ulong[100];
			//We choose 15 as anything above this takes too long
			IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);

			for (int j = 0; j < 100; j++)
			{
				//Countsketch
				ulong res3 = Tests.TestCount(stream, m);
				//Modprime S

				Console.WriteLine("Count Sketch Squared Sum Estimate : " + res3.ToString() + "\n");
				Xarr[j] = res3;
				//float dif = (float)res1 - (float)res3;
				//Console.WriteLine("\nDifference: " + dif.ToString() + "\n");
			}

			//Our S value from our MultiModPrime
			ulong S = Tests.TestSqredSum(stream, HashFuncts.MultiShift, l);

			//Mean Square Error
			float MSE = CountSketch.MSE(Xarr, S);
			float varians = CountSketch.Var(S, m);
			Console.WriteLine("MSE : " + MSE.ToString() + "\n");
			Console.WriteLine("Var : " + varians.ToString() + "\n");

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

			//Sort the array into ascending order
			Array.Sort(MedianArr);

			foreach (float item in MedianArr)
			{
				Console.WriteLine("MedianArr : " + item.ToString());
			}
			Console.WriteLine(S.ToString());
		}
	}
}
