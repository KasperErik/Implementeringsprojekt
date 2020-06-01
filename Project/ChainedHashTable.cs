using System;
using System.Collections.Generic;

namespace Project
{
	public struct KeyValue
	{
		public ulong Key
		{
			get; set;
		}

		public int Value
		{
			get; set;
		}
	}

	public class ChainedHashTable
	{
		private readonly int size;
		public readonly LinkedList<KeyValue>[] items;
		private readonly Func<ulong, int, ulong> myHash;

		public ChainedHashTable(Func<ulong, int, ulong> myHash, int size)
		{
			this.myHash = myHash;
			this.size = size;
			//billedmængde 1 << size , where size = l
			items = new LinkedList<KeyValue>[1UL << size];
		}

		protected ulong GetArrayPosition(ulong key)
		{
			return myHash(key, size);
		}

		public int Get(ulong key)
		{
			ulong position = GetArrayPosition(key);
			LinkedList<KeyValue> linkedList = GetLinkedList(position);
			foreach (KeyValue item in linkedList)
			{
				if (item.Key.Equals(key))
				{
					return item.Value;
				}
			}
			return default;
		}

		private void Add(ulong key, int value)
		{
			ulong position = GetArrayPosition(key);
			LinkedList<KeyValue> linkedList = GetLinkedList(position);
			KeyValue item = new KeyValue() { Key = key, Value = value };
			linkedList.AddLast(item);
		}

		public void Set(ulong key, int value)
		{
			ulong position = GetArrayPosition(key);
			LinkedList<KeyValue> linkedList = GetLinkedList(position);
			bool itemFound = false;
			KeyValue foundItem = default;
			foreach (KeyValue item in linkedList)
			{
				if (item.Key.Equals(key))
				{
					itemFound = true;
					foundItem = item;
				}
			}
			if (itemFound)
			{
				linkedList.Remove(foundItem);
			}
			Add(key, value);
		}

		public void Increment(ulong key, int d)
		{
			ulong position = GetArrayPosition(key);
			LinkedList<KeyValue> linkedList = GetLinkedList(position);

			bool itemFound = false;
			KeyValue foundItem = default;

			foreach (KeyValue item in linkedList)
			{
				if (item.Key.Equals(key))
				{
					itemFound = true;
					foundItem = item;
				}
			}

			int value = default;
			if (itemFound)
			{
				value = foundItem.Value;
				linkedList.Remove(foundItem);
			}
			Add(key, d + value);
		}

		protected LinkedList<KeyValue> GetLinkedList(ulong position)
		{
			LinkedList<KeyValue> linkedList = items[position];

			if (linkedList == null)
			{
				linkedList = new LinkedList<KeyValue>();
				items[position] = linkedList;
			}

			return linkedList;
		}

		public override string ToString()
		{
			string res = "";
			foreach (LinkedList<KeyValue> item in items)
			{
				if (item != null)
				{
					foreach (KeyValue j in item)
					{
						res += j.ToString();
						res += "\n";
					}
				}
			}
			return res;
		}
	}
}