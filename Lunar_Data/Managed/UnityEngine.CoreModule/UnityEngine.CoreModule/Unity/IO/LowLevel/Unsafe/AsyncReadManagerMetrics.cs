using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000081 RID: 129
	[NativeConditional("ENABLE_PROFILER")]
	public static class AsyncReadManagerMetrics
	{
		// Token: 0x06000200 RID: 512
		[FreeFunction("AreMetricsEnabled_Internal")]
		[MethodImpl(4096)]
		public static extern bool IsEnabled();

		// Token: 0x06000201 RID: 513
		[FreeFunction("GetAsyncReadManagerMetrics()->ClearMetrics")]
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern void ClearMetrics_Internal();

		// Token: 0x06000202 RID: 514 RVA: 0x00003F60 File Offset: 0x00002160
		public static void ClearCompletedMetrics()
		{
			AsyncReadManagerMetrics.ClearMetrics_Internal();
		}

		// Token: 0x06000203 RID: 515
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Internal(bool clear);

		// Token: 0x06000204 RID: 516
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc")]
		[MethodImpl(4096)]
		internal static extern void GetMetrics_NoAlloc_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, bool clear);

		// Token: 0x06000205 RID: 517
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics_Filtered_Managed")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Filtered_Internal(AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x06000206 RID: 518
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc_Filtered_Managed")]
		[MethodImpl(4096)]
		internal static extern void GetMetrics_NoAlloc_Filtered_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x06000207 RID: 519 RVA: 0x00003F6C File Offset: 0x0000216C
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Filtered_Internal(filters, flag);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00003F90 File Offset: 0x00002190
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Filtered_Internal(outMetrics, filters, flag);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Internal(flag);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Internal(outMetrics, flag);
		}

		// Token: 0x0600020B RID: 523
		[FreeFunction("GetAsyncReadManagerMetrics()->StartCollecting")]
		[MethodImpl(4096)]
		public static extern void StartCollectingMetrics();

		// Token: 0x0600020C RID: 524
		[FreeFunction("GetAsyncReadManagerMetrics()->StopCollecting")]
		[MethodImpl(4096)]
		public static extern void StopCollectingMetrics();

		// Token: 0x0600020D RID: 525
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetrics")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetrics_Internal(bool clear);

		// Token: 0x0600020E RID: 526 RVA: 0x00003FFC File Offset: 0x000021FC
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetrics_Internal(flag);
		}

		// Token: 0x0600020F RID: 527
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetricsWithFilters")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetricsWithFilters_Internal(AsyncReadManagerMetricsFilters metricsFilters, bool clear);

		// Token: 0x06000210 RID: 528 RVA: 0x00004020 File Offset: 0x00002220
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetricsFilters metricsFilters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetricsWithFilters_Internal(metricsFilters, flag);
		}

		// Token: 0x06000211 RID: 529
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_Managed")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_Internal(AsyncReadManagerRequestMetric[] metrics);

		// Token: 0x06000212 RID: 530 RVA: 0x00004044 File Offset: 0x00002244
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_Internal(metrics);
		}

		// Token: 0x06000213 RID: 531
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_FromContainer_Managed", ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics);

		// Token: 0x06000214 RID: 532 RVA: 0x0000405C File Offset: 0x0000225C
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_FromContainer_Internal(metrics);
		}

		// Token: 0x06000215 RID: 533
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_Managed")]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_Internal(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x06000216 RID: 534 RVA: 0x00004074 File Offset: 0x00002274
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_Internal(metrics, metricsFilters);
		}

		// Token: 0x06000217 RID: 535
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_FromContainer_Managed", ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x06000218 RID: 536 RVA: 0x00004090 File Offset: 0x00002290
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_FromContainer_Internal(metrics, metricsFilters);
		}

		// Token: 0x06000219 RID: 537
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetTotalSizeNonASRMReadsBytes")]
		[MethodImpl(4096)]
		public static extern ulong GetTotalSizeOfNonASRMReadsBytes(bool emptyAfterRead);

		// Token: 0x02000082 RID: 130
		[Flags]
		public enum Flags
		{
			// Token: 0x040001EC RID: 492
			None = 0,
			// Token: 0x040001ED RID: 493
			ClearOnRead = 1
		}
	}
}
