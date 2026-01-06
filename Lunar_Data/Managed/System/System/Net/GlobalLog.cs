using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x02000441 RID: 1089
	internal static class GlobalLog
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x0007EA6D File Offset: 0x0007CC6D
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00003062 File Offset: 0x00001262
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0007EA74 File Offset: 0x0007CC74
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("TRAVE")]
		public static void Print(string msg)
		{
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0007EA98 File Offset: 0x0007CC98
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num != -1)
				{
					int length = text.Length;
				}
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00003917 File Offset: 0x00001B17
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		public static void Assert(string message)
		{
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0007EACC File Offset: 0x0007CCCC
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(string message, string detailMessage)
		{
			try
			{
				GlobalLog.Logobject.DumpArray(false);
			}
			finally
			{
				Debugger.Break();
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x0400141A RID: 5146
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}
