using System;
using System.Collections.Generic;

namespace Project
{
	internal class SquaredSum
	{
		public static ulong SquareSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			FixedSizeGenericHashTable hash = new FixedSizeGenericHashTable(f, size);
			foreach (var x in stream)
			{
				hash.Increment(x.Item1, x.Item2);
			}
			ulong S = 0;
			for (int i = 0; i < size; i++)
			{
				if (hash.items[i] != null)
				{
					foreach (KeyValue item in hash.items[i])
					{
						S += (ulong)(item.Value * item.Value);
					}
				}
			}
			return S;
		}
	}
}