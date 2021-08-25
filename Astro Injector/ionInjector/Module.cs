using System;
using System.Windows.Forms;

namespace ionInjector
{
	// Token: 0x02000005 RID: 5
	public class Module
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020FC File Offset: 0x000002FC
		public void InjectDLL(string _proc, byte[] sdllbytes)
		{
			switch (DLLInjection.DllInjector.GetInstance.Inject(_proc, sdllbytes))
			{
			case DLLInjection.DllInjectionResult.DllNotFound:
			{
				int num = (int)MessageBox.Show("Couldn't find the dll!", "Error: Dll Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			case DLLInjection.DllInjectionResult.GameProcessNotFound:
			{
				int num2 = (int)MessageBox.Show("Process does not exist", "Apllication Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			case DLLInjection.DllInjectionResult.InjectionFailed:
			{
				int num3 = (int)MessageBox.Show("Injection failed! :(", "Injection Failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			case DLLInjection.DllInjectionResult.Success:
				Console.WriteLine("Menu Is Injected! Don't Close The injector it will close it self!");
				break;
			}
		}
	}
}
