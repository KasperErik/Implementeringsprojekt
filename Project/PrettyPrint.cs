using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
	class PrettyPrint
	{
		/*
		 * ╔═════╗
		 * ║65001║
		 * ╠═════╣
		 * ╚═════╝
		 * 
		 */
		private static string CenterWithSpace(string s, int width)
		{
			if (s.Length >= width)
			{
				return s;
			}

			int leftPadding = (width - s.Length) / 2;
			int rightPadding = width - s.Length - leftPadding;

			return new string(' ', leftPadding) + s + new string(' ', rightPadding);
		}
		private static string CenterWithLine(string s, int width)
		{
			if (s.Length >= width)
			{
				return s;
			}

			int leftPadding = (width - s.Length) / 2;
			int rightPadding = width - s.Length - leftPadding;

			return new string('═', leftPadding - 1) + ' ' + s + ' ' + new string('═', rightPadding - 1);
		}

		public static void Header(string str)
		{
			str = CenterWithSpace(str, 34);
			Console.WriteLine(" ╔════════════════════════════════════╗");
			Console.WriteLine(" ║ {0,-34} ║", str);
			Console.WriteLine(" ╠════════════════════════════════════╣");
		}

		public static void Footer(string str)
		{
			str = CenterWithSpace(str, 34);
			Console.WriteLine(" ╠════════════════════════════════════╣");
			Console.WriteLine(" ║ {0,-34} ║", str);
			Console.WriteLine(" ╚════════════════════════════════════╝\n");
		}

		public static void Spacer()
		{
			Console.WriteLine(" ║                                    ║");
		}

		public static void SubSect(string str)
		{
			str = CenterWithLine(str, 34);
			Spacer();
			Console.WriteLine(" ╠═{0,-34}═╣", str);
			Spacer();
		}

		public static void Body(string str1, string str2)
		{
			Console.WriteLine(" ║ {0,-13} {1,20} ║", str1, str2);
		}

		public static void Body(string str)
		{
			str = CenterWithSpace(str, 34);
			Console.WriteLine(" ║ {0,-34} ║", str);
		}

		public static void Body(int i)
		{
			Console.WriteLine(" ║ {0,34} ║", i);
		}
	}
}
