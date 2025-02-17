﻿using System;
using System.Numerics;
using System.Collections.Generic;

public class CountSketch
{
	public static int[] Sketch(Func<ulong, BigInteger> g, Func<BigInteger, int, Tuple<int, int>> sAndH, IEnumerable<Tuple<ulong, int>> stream, int m)
	{
		int[] SketchArr = new int[m];
		foreach (Tuple<ulong, int> item in stream)
		{
			BigInteger x = g(item.Item1);
			int d = item.Item2;
			Tuple<int, int> res = sAndH(x, m);
			int s = res.Item1;
			int h = res.Item2;
			SketchArr[h] = SketchArr[h] + (s * d);
		}
		return SketchArr;
	}

	public static ulong Estimate(int[] SketchArr)
	{
		ulong res = 0;
		foreach (int item in SketchArr)
		{
			res += (ulong)(item * item);
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

	public static float[] GetMediaArray(ulong[] Xarr)
	{
		List<ulong[]> List = DivideIntoSubArr(Xarr);

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

		return MedianArr;
	}
}
