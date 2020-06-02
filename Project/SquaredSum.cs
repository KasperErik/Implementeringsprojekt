using System;
using System.Collections.Generic;

namespace Project
{
	internal class SquaredSum
	{
		public static ChainedHashTable FillHashtable(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> f, int size)
		{
			ChainedHashTable hash = new ChainedHashTable(f, size);
			foreach (var x in stream)
			{
				hash.Increment(x.Item1, x.Item2);
			}
            return hash;
		}
		public static ulong SquareSum(ChainedHashTable hash, int size)
		{
			ulong S = 0;
			for (ulong i = 0; i < (1UL << size); i++)
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