using System;
using System.Collections.Generic;
using System.IO;

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

			string docPath = Directory.GetCurrentDirectory();

			// Write the string array to a new file named "WriteLines.txt".

			StreamWriter outputFile1 = new StreamWriter(Path.Combine(docPath, "Opgave3Shift.txt"));
			outputFile1.WriteLine("Multi Shift hello");

			StreamWriter outputFile2 = new StreamWriter(Path.Combine(docPath, "Opgave3ModP.txt"));
			outputFile2.WriteLine("Mod Prime hello");

			for (int i = 1; i < maxL; i++)
			{
				PrettyPrint.SubSect("Using L = " + i);

				stream = Stream.CreateStream(n, i);

				(ulong sum1, long time1) = Tests.TestSqredSum(stream, HashFuncts.MultiShift, i);

				outputFile1.WriteLine($"{i}, {time1}");

				PrettyPrint.Body("Shift sum  : ", sum1.ToString(""));
				PrettyPrint.Body("Time in ms :", time1.ToString());
				PrettyPrint.Spacer();

				(ulong sum2, long time2) = Tests.TestSqredSum(stream, HashFuncts.MultiModPrime, i);

				outputFile2.WriteLine($"{i}, {time2}");

				PrettyPrint.Body("Mod p sum  : ", sum2.ToString());
				PrettyPrint.Body("Time in ms :", time2.ToString());
				PrettyPrint.Spacer();

				float dif = (float)time2 / (float)time1;

				PrettyPrint.Body("Difference :", dif.ToString("F"));
			}

			outputFile1.Close();
			outputFile2.Close();

			PrettyPrint.Footer("Assignment 3 Is Done");
		}

		public static void Opgave7(int n, int l, int[] ms)
		{
			PrettyPrint.Header("Running Assignment 7 & 8");
			foreach (int m in ms)
			{
				IEnumerable<Tuple<ulong, int>> stream = Stream.CreateStream(n, l);


				Tests.verbatim = false; //this is just to stop it from printing the estimates.

				//Estimate values from count sketch
				(ulong[] Xarr, long time1) = Tests.TestCount(stream, m);

				//Create new array for the purpose of writing 100 points to file to compare with S
				ulong[] SortedXarr = Xarr;
				Array.Sort(SortedXarr);

				string docPath = Directory.GetCurrentDirectory();

				// Write the string array to a new file named "Opgave7.1.txt".
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"Opgave7.1m{m}.txt")))
				{
					for (int i = 0; i < SortedXarr.Length; i++)
					{
						outputFile.WriteLine($"{i + 1}, {SortedXarr[i]}");
					}
				}

				//Our S value from our MultiShift hashfunction
				(ulong S, long time2) = Tests.TestSqredSum(stream, HashFuncts.MultiShift, l);
				float timeres = (float)time1 / 100.0f;
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"Opgave7.1m{m}.txt"), true))
				{
					outputFile.WriteLine($"\nCount Sketch Time = {timeres} from {time1}/100");
					outputFile.WriteLine($"\nS = {S}");
					outputFile.WriteLine($"\nTime With Chaining= {time2}");
				}

				//Mean Square Error
				float MSE = CountSketch.MSE(Xarr, S);
				//Expected Varians of X
				float varians = CountSketch.Var(S, m);

				//Get the median of our new 9 arrayes
				float[] MedianArr = CountSketch.GetMediaArray(Xarr);

				// Write the string array to a new file named "Opgave7.2.txt".
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"Opgave7.2m{m}.txt")))
				{
					for (int i = 0; i < MedianArr.Length; i++)
					{
						outputFile.WriteLine($"{i + 1}, {MedianArr[i]}");
					}
				}
				//Print the values in console
				PrettyPrint.SubSect("MSE and Var");
				PrettyPrint.Body("MSE : ", MSE.ToString());
				PrettyPrint.Body("Var : ", varians.ToString());
				PrettyPrint.SubSect("Median values");
				foreach (float item in MedianArr)
				{
					PrettyPrint.Body("MedianArr : ", item.ToString());
				}
				PrettyPrint.Spacer();
				PrettyPrint.Body("Median    :", MedianArr[4].ToString());
				PrettyPrint.Spacer();
				PrettyPrint.Body("Real sum  :", S.ToString());
				PrettyPrint.Spacer();
			}
			PrettyPrint.Footer("Assignment 7 & 8 Is Done");
		}
	}
}
