using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
	class SquaredSum
	{
		public ulong SquareSum(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, ulong> f, int size)
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
						S += (ulong)Math.Pow(item.Value, 2);
					}
				}
			}
			return S;
		}
	}
}
