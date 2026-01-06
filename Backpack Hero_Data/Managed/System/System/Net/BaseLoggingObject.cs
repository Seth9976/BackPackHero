using System;

namespace System.Net
{
	// Token: 0x0200043F RID: 1087
	internal class BaseLoggingObject
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x0000219B File Offset: 0x0000039B
		internal BaseLoggingObject()
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush()
		{
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
