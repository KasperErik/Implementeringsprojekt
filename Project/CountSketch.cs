using System;
using System.Collections.Generic;

public class CountSketch
{
	public static ulong[] Sketch(Func<ulong, int, ulong> g, Func<ulong, int, Tuple<ulong, ulong>> hAndS, IEnumerable<Tuple<ulong, int>> stream, int l)
	{
		ulong[] SketchArr = new ulong[1 << l];
		foreach (var item in stream)
		{
			ulong x = g(item.Item1, l);
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
}
