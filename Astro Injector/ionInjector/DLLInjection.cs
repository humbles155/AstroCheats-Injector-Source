using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace ionInjector
{
	// Token: 0x02000004 RID: 4
	internal class DLLInjection
	{
		// Token: 0x04000004 RID: 4
		public static string dname;

		// Token: 0x04000005 RID: 5
		public static bool AsjdhIJBHYsus8a;

		// Token: 0x02000008 RID: 8
		public enum DllInjectionResult
		{
			// Token: 0x0400000D RID: 13
			DllNotFound,
			// Token: 0x0400000E RID: 14
			GameProcessNotFound,
			// Token: 0x0400000F RID: 15
			InjectionFailed,
			// Token: 0x04000010 RID: 16
			Success
		}

		// Token: 0x02000009 RID: 9
		public sealed class DllInjector
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000024D4 File Offset: 0x000006D4
			public static DLLInjection.DllInjector GetInstance
			{
				get
				{
					bool flag = DLLInjection.DllInjector._instance == null;
					if (flag)
					{
						DLLInjection.DllInjector._instance = new DLLInjection.DllInjector();
					}
					return DLLInjection.DllInjector._instance;
				}
			}

			// Token: 0x06000013 RID: 19 RVA: 0x00002501 File Offset: 0x00000701
			private DllInjector()
			{
			}

			// Token: 0x06000014 RID: 20 RVA: 0x0000250C File Offset: 0x0000070C
			private bool bInject(uint pToBeInjected, string sDllPath)
			{
				IntPtr intPtr = DLLInjection.DllInjector.OpenProcess(1082U, 1, pToBeInjected);
				bool flag = intPtr == DLLInjection.DllInjector.INTPTR_ZERO;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					IntPtr procAddress = DLLInjection.DllInjector.GetProcAddress(DLLInjection.DllInjector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
					bool flag2 = procAddress == DLLInjection.DllInjector.INTPTR_ZERO;
					if (flag2)
					{
						result = false;
					}
					else
					{
						IntPtr intPtr2 = DLLInjection.DllInjector.VirtualAllocEx(intPtr, (IntPtr)0, (IntPtr)sDllPath.Length, 12288U, 64U);
						bool flag3 = intPtr2 == DLLInjection.DllInjector.INTPTR_ZERO;
						if (flag3)
						{
							result = false;
						}
						else
						{
							byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
							bool flag4 = DLLInjection.DllInjector.WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, 0) == 0 || DLLInjection.DllInjector.CreateRemoteThread(intPtr, (IntPtr)0, DLLInjection.DllInjector.INTPTR_ZERO, procAddress, intPtr2, 0U, (IntPtr)0) == DLLInjection.DllInjector.INTPTR_ZERO;
							if (flag4)
							{
								result = false;
							}
							else
							{
								DLLInjection.DllInjector.CloseHandle(intPtr);
								result = true;
							}
						}
					}
				}
				return result;
			}

			// Token: 0x06000015 RID: 21
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int CloseHandle(IntPtr hObject);

			// Token: 0x06000016 RID: 22
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

			// Token: 0x06000017 RID: 23
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetModuleHandle(string lpModuleName);

			// Token: 0x06000018 RID: 24
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

			// Token: 0x06000019 RID: 25 RVA: 0x00002600 File Offset: 0x00000800
			private static byte[] GetFileHash(string fileName)
			{
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create();
				byte[] result;
				using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				{
					result = hashAlgorithm.ComputeHash(fileStream);
				}
				return result;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002644 File Offset: 0x00000844
			private static string CreateTemporaryDll(byte[] dllBytes)
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "Sqwyuwegsd", "UWU"));
				foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles())
				{
					try
					{
						fileInfo.Delete();
					}
					catch (Exception)
					{
					}
				}
				string randomFileName = Path.GetRandomFileName();
				string text = Path.Combine(directoryInfo.FullName, randomFileName + ".dll");
				try
				{
					File.WriteAllBytes(text, dllBytes);
				}
				catch (IOException)
				{
				}
				DLLInjection.dname = randomFileName;
				return text;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x00002714 File Offset: 0x00000914
			public DLLInjection.DllInjectionResult Inject(string sProcName, byte[] dllBytes)
			{
				uint num = 0U;
				Process[] processes = Process.GetProcesses();
				for (int i = 0; i < processes.Length; i++)
				{
					bool flag = !(processes[i].ProcessName != sProcName);
					if (flag)
					{
						num = (uint)processes[i].Id;
						break;
					}
				}
				bool flag2 = num == 0U;
				DLLInjection.DllInjectionResult result;
				if (flag2)
				{
					result = DLLInjection.DllInjectionResult.GameProcessNotFound;
				}
				else
				{
					result = ((!this.bInject(num, DLLInjection.DllInjector.CreateTemporaryDll(dllBytes))) ? DLLInjection.DllInjectionResult.InjectionFailed : DLLInjection.DllInjectionResult.Success);
				}
				return result;
			}

			// Token: 0x0600001C RID: 28
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

			// Token: 0x0600001D RID: 29
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

			// Token: 0x0600001E RID: 30
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

			// Token: 0x04000011 RID: 17
			private static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

			// Token: 0x04000012 RID: 18
			private static DLLInjection.DllInjector _instance;
		}
	}
}
