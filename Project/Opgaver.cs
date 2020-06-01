using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
	class Opgaver
	{
		public static void Opgave1(int n, int l)
		{
			PrettyPrint.Header("Running Assignment 1");

			IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);

			long a = Tests.HashFunctionTest(stream, HashFuncts.MultiShift, l);
			long b = Tests.HashFunctionTest(stream, HashFuncts.MultiModPrime, l);

			PrettyPrint.Body("Shift time :", a.ToString());
			PrettyPrint.Body("Mod P time :", b.ToString());

			float res = (float)b / (float)a;

			PrettyPrint.Body("Difference :", res.ToString());

			PrettyPrint.Footer("Assignment 1 Is Done");
		}

		public static void Opgave3(int n, int maxL)
		{
			PrettyPrint.Header("Running Assignment 3");

			IEnumerable<Tuple<ulong, int>> stream = default;

			for (int i = 1; i < maxL; i++)
			{
				PrettyPrint.SubSect("Using L = " + i);

				stream = Stream.CreateStream(n, i);
				ulong res1 = Tests.TestSqredSum(stream, HashFuncts.MultiShift, i);

				PrettyPrint.Body("Shift sum  : ", res1.ToString(""));
				PrettyPrint.Spacer();

				ulong res2 = Tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

				PrettyPrint.Body("Mod p sum  : ", res2.ToString());
			}

			PrettyPrint.Footer("Assignment 3 Is Done");
		}

		public static void Opgave7(int n, int l, int m)
		{
			PrettyPrint.Header("Running Assignment 7");

			IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);

			//Estimate values from count sketch
			ulong[] Xarr = Tests.TestCount(stream, m);

			//Our S value from our MultiModPrime
			ulong S = Tests.TestSqredSum(stream, HashFuncts.MultiShift, l);

			//Mean Square Error
			float MSE = CountSketch.MSE(Xarr, S);
			float varians = CountSketch.Var(S, m);

			PrettyPrint.SubSect("MSE and Var");
			PrettyPrint.Body("MSE : ", MSE.ToString());
			PrettyPrint.Body("Var : ", varians.ToString());

			PrettyPrint.SubSect("Median values");

			float[] MedianArr = CountSketch.GetMediaArray(Xarr);

			foreach (float item in MedianArr)
			{
				PrettyPrint.Body("MedianArr : ", item.ToString());
			}

			PrettyPrint.Spacer();
			PrettyPrint.Body("Real sum  :", S.ToString());
			PrettyPrint.Footer("Assignment 7 Is Done");
		}
	}
}
