using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace ionInjector
{
	// Token: 0x02000007 RID: 7
	internal static class Program
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		private static void Main(string[] args)
		{
			Console.WriteLine("Finding File");
			Thread.Sleep(1000);
			byte[] array = File.ReadAllBytes("C:\\Astro.dll");
			Console.WriteLine("File Found");
			Thread.Sleep(1000);
			Console.WriteLine("Injecting");
			Thread.Sleep(1000);
			Program.module.InjectDLL("GTA5", array);
			Console.WriteLine("Injected");
			Thread.Sleep(99999999);

		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002430 File Offset: 0x00000630
		public static byte[] DeCrypt(byte[] Bytes)
		{
			List<byte> list = new List<byte>(Bytes);
			List<byte> list2 = new List<byte>();
			byte b = (byte)(list.Last<byte>() ^ list.ElementAt(list.Count - 2));
			list2.Add(b);
			for (int i = 0; i < list.Count - 1; i++)
			{
				bool flag = i == list.Count - 2;
				b = ((byte)(list.ElementAt(i) ^ b));
				bool flag2 = !flag;
				if (flag2)
				{
					list2.Add(b);
				}
			}
			Bytes = list2.ToArray();
			list2.Clear();
			Console.WriteLine("Decrypted");
			return Bytes;
		}

		// Token: 0x04000009 RID: 9
		public static Module module = new Module();

		// Token: 0x0400000A RID: 10
		public static string AuthToken;

		// Token: 0x0400000B RID: 11
		public static int ProductID;
	}
}
