using System;
using System.Diagnostics;

namespace System.Net
{
	// Token: 0x02000477 RID: 1143
	internal static class Logging
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool On
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource Web
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource HttpListener
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x00002F6A File Offset: 0x0000116A
		internal static TraceSource Sockets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, object obj, string method, object paramObject)
		{
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Enter(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exception(TraceSource traceSource, object obj, string method, Exception e)
		{
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, object obj, string method, object retObject)
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void Exit(TraceSource traceSource, string msg, string parameters)
		{
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, object obj, string msg)
		{
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintInfo(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, object obj, string method, string msg)
		{
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintWarning(TraceSource traceSource, string msg)
		{
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("TRACE")]
		internal static void PrintError(TraceSource traceSource, string msg)
		{
		}
	}
}
