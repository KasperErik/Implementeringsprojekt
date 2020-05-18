using System;
using System.Collections.Generic;

namespace Project
{
	public struct KeyValue<K, V>
	{
		public K Key
		{
			get; set;
		}
		public V Value
		{
			get; set;
		}
	}

	public class FixedSizeGenericHashTable<K, V> where K : IComparable<K> where V : IComparable
	{
		private readonly int size;
		private readonly LinkedList<KeyValue<K, V>>[] items;
		private readonly Func<K, K> myHash;
		public FixedSizeGenericHashTable(Func<K, K> myHash, int size)
		{
			this.myHash = myHash;
			this.size = size;
			items = new LinkedList<KeyValue<K, V>>[1 << size];
		}

		public int Mod(K key, int size)
		{
			return (int)(ulong.Parse(key.ToString()) % (ulong)size);
		}
		public static V Add(V number1, V number2)
		{
			dynamic a = number1;
			dynamic b = number2;
			return a + b;
		}

		protected int GetArrayPosition(K key)
		{
			int position = Mod(myHash(key), size);
			//Console.WriteLine("Array Postion: {0}, key: {1}, size: {2}, hashcode: {3}", position, key, size, myHash);
			return int.Parse(position.ToString());
		}

		public V Get(K key)
		{
			int position = GetArrayPosition(key);
			LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
			foreach (KeyValue<K, V> item in linkedList)
			{
				if (item.Key.Equals(key))
				{
					return item.Value;
				}
			}

			return default;
		}

		private void Add(K key, V value)
		{
			int position = GetArrayPosition(key);
			LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
			KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };
			linkedList.AddLast(item);
		}

		public void Set(K key, V value)
		{
			int position = GetArrayPosition(key);
			LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
			bool itemFound = false;
			KeyValue<K, V> foundItem = default;
			foreach (KeyValue<K, V> item in linkedList)
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

		public void Increment(K key, V d)
		{
			int position = GetArrayPosition(key);
			LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
			bool itemFound = false;
			KeyValue<K, V> foundItem = default;
			foreach (KeyValue<K, V> item in linkedList)
			{
				if (item.Key.Equals(key))
				{
					itemFound = true;
					foundItem = item;
				}
			}
			V value = default;
			if (itemFound)
			{
				value = foundItem.Value;
				linkedList.Remove(foundItem);
			}
			Add(key, Add(d, value));
		}

		public void Remove(K key)
		{
			int position = GetArrayPosition(key);
			LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
			bool itemFound = false;
			KeyValue<K, V> foundItem = default;
			foreach (KeyValue<K, V> item in linkedList)
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
		}

		protected LinkedList<KeyValue<K, V>> GetLinkedList(int position)
		{
			LinkedList<KeyValue<K, V>> linkedList = items[position];
			if (linkedList == null)
			{
				linkedList = new LinkedList<KeyValue<K, V>>();
				items[position] = linkedList;
			}

			return linkedList;
		}
		public override string ToString()
		{
			string res = "";
			foreach (LinkedList<KeyValue<K, V>> item in items)
			{
				if (item != null)
				{
					foreach (KeyValue<K, V> j in item)
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