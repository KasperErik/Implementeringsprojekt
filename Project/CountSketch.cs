using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CountSketch
{
	public static ulong[] Sketch(Func<ulong, BigInteger> g, Func<BigInteger, int, Tuple<int, ulong>> sAndH, IEnumerable<Tuple<ulong, int>> stream, int m)
	{
		ulong[] SketchArr = new ulong[m];
		foreach (Tuple<ulong, int> item in stream)
		{
			BigInteger x = g(item.Item1);
			int d = item.Item2;
			//Console.WriteLine(x.ToString("N0"));
			Tuple<int, ulong> res = sAndH(x, m);
			int s = res.Item1;
			ulong h = res.Item2;
			SketchArr[h] = SketchArr[h] + (ulong)(s * d);
		}
		return SketchArr;
	}

	public static ulong Estimate(ulong[] SketchArr)
	{
		ulong res = 0;
		foreach (ulong item in SketchArr)
		{
			res += item * item;
		}
		return res;
	}

	public static float MSE(ulong[] Xarr, ulong S)
	{
		ulong sqrSum = 0;
		foreach (ulong item in Xarr)
		{
			sqrSum += (item - S) * (item - S);
		}
		return (float)sqrSum / (float)Xarr.Length;
	}
	public static float Var(ulong S, int m)
	{
		return (float)(S * S * 2) / (float)m;
	}

	public static List<ulong[]> DivideIntoSubArr(ulong[] Xarr)
	{
		List<ulong[]> res = new List<ulong[]>();
		for (int i = 0; i < 9; i++)
		{
			ulong[] temp = new ulong[11];
			for (int j = 0; j < 11; j++)
			{
				temp[j] = Xarr[(i * 11) + j];
			}
			res.Add(temp);
		}
		return res;
	}
}
