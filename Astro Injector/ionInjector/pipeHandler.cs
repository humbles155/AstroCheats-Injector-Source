using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace ionInjector
{
	// Token: 0x02000006 RID: 6
	public class pipeHandler
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002188 File Offset: 0x00000388
		public pipeHandler()
		{
			this.pipeServer = new NamedPipeServerStream("myNamedPipe1");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A2 File Offset: 0x000003A2
		public void establishConnection()
		{
			this.re = new StreamReader(this.pipeServer);
			this.wr = new StreamWriter(this.pipeServer);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C7 File Offset: 0x000003C7
		public void writePipe(string text)
		{
			this.wr.Write(text);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D8 File Offset: 0x000003D8
		public string readPipe()
		{
			bool flag = this.re.Peek() == -1;
			if (flag)
			{
				Thread.Sleep(2000);
			}
			bool flag2 = this.re.Peek() > -1;
			string result;
			if (flag2)
			{
				string text = this.re.ReadToEnd();
				result = text;
			}
			else
			{
				result = "fail";
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		private StreamReader re;

		// Token: 0x04000007 RID: 7
		private StreamWriter wr;

		// Token: 0x04000008 RID: 8
		private NamedPipeServerStream pipeServer;
	}
}
