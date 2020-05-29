using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CountSketch
{
	public static ulong[] Sketch(Func<ulong, int, BigInteger> g, Func<BigInteger, int, Tuple<ulong, ulong>> hAndS, IEnumerable<Tuple<ulong, int>> stream, int l)
	{
		ulong[] SketchArr = new ulong[1 << l];
		foreach (var item in stream)
		{
			BigInteger x = g(item.Item1, l);
			Tuple<ulong, ulong> res = hAndS(x, l);
			ulong s = res.Item1;
			ulong h = res.Item2;
			SketchArr[h] = SketchArr[h] + s * (ulong)item.Item2;
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
		float MSE = default;
		foreach (var item in Xarr)
		{
			ulong temp = item - S;
			MSE += (float)(temp * temp) / (float)Xarr.Length;
		}
		return MSE;
	}
	public static float Var(ulong S, ulong m)
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
