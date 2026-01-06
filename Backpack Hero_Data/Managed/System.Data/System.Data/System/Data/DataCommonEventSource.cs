using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace System.Data
{
	// Token: 0x0200003D RID: 61
	[EventSource(Name = "System.Data.DataCommonEventSource")]
	internal class DataCommonEventSource : EventSource
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000CF06 File Offset: 0x0000B106
		[Event(1, Level = EventLevel.Informational)]
		internal void Trace(string message)
		{
			base.WriteEvent(1, message);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CF10 File Offset: 0x0000B110
		[NonEvent]
		internal void Trace<T0>(string format, T0 arg0)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CF31 File Offset: 0x0000B131
		[NonEvent]
		internal void Trace<T0, T1>(string format, T0 arg0, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CF58 File Offset: 0x0000B158
		[NonEvent]
		internal void Trace<T0, T1, T2>(string format, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CF88 File Offset: 0x0000B188
		[NonEvent]
		internal void Trace<T0, T1, T2, T3>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D038 File Offset: 0x0000B238
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4, T5, T6>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 }));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		[Event(2, Level = EventLevel.Verbose)]
		internal long EnterScope(string message)
		{
			long num = 0L;
			if (DataCommonEventSource.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref DataCommonEventSource.s_nextScopeId);
				base.WriteEvent(2, num, message);
			}
			return num;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
		[NonEvent]
		internal long EnterScope<T1>(string format, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		[NonEvent]
		internal long EnterScope<T1, T2>(string format, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D125 File Offset: 0x0000B325
		[NonEvent]
		internal long EnterScope<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2, arg3));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D158 File Offset: 0x0000B358
		[NonEvent]
		internal long EnterScope<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, new object[] { arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		[Event(3, Level = EventLevel.Verbose)]
		internal void ExitScope(long scopeId)
		{
			base.WriteEvent(3, scopeId);
		}

		// Token: 0x04000487 RID: 1159
		internal static readonly DataCommonEventSource Log = new DataCommonEventSource();

		// Token: 0x04000488 RID: 1160
		private static long s_nextScopeId = 0L;

		// Token: 0x04000489 RID: 1161
		private const int TraceEventId = 1;

		// Token: 0x0400048A RID: 1162
		private const int EnterScopeId = 2;

		// Token: 0x0400048B RID: 1163
		private const int ExitScopeId = 3;
	}
}
