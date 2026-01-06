using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000083 RID: 131
	[NativeAsStruct]
	[NativeConditional("ENABLE_PROFILER")]
	[StructLayout(0)]
	public class AsyncReadManagerSummaryMetrics
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000040A9 File Offset: 0x000022A9
		[NativeName("totalBytesRead")]
		public ulong TotalBytesRead { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000040B1 File Offset: 0x000022B1
		[NativeName("averageBandwidthMBPerSecond")]
		public float AverageBandwidthMBPerSecond { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000040B9 File Offset: 0x000022B9
		[NativeName("averageReadSizeInBytes")]
		public float AverageReadSizeInBytes { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000040C1 File Offset: 0x000022C1
		[NativeName("averageWaitTimeMicroseconds")]
		public float AverageWaitTimeMicroseconds { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000040C9 File Offset: 0x000022C9
		[NativeName("averageReadTimeMicroseconds")]
		public float AverageReadTimeMicroseconds { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000040D1 File Offset: 0x000022D1
		[NativeName("averageTotalRequestTimeMicroseconds")]
		public float AverageTotalRequestTimeMicroseconds { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000040D9 File Offset: 0x000022D9
		[NativeName("averageThroughputMBPerSecond")]
		public float AverageThroughputMBPerSecond { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000040E1 File Offset: 0x000022E1
		[NativeName("longestWaitTimeMicroseconds")]
		public float LongestWaitTimeMicroseconds { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000040E9 File Offset: 0x000022E9
		[NativeName("longestReadTimeMicroseconds")]
		public float LongestReadTimeMicroseconds { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000040F1 File Offset: 0x000022F1
		[NativeName("longestReadAssetType")]
		public ulong LongestReadAssetType { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000040F9 File Offset: 0x000022F9
		[NativeName("longestWaitAssetType")]
		public ulong LongestWaitAssetType { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00004101 File Offset: 0x00002301
		[NativeName("longestReadSubsystem")]
		public AssetLoadingSubsystem LongestReadSubsystem { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00004109 File Offset: 0x00002309
		[NativeName("longestWaitSubsystem")]
		public AssetLoadingSubsystem LongestWaitSubsystem { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00004111 File Offset: 0x00002311
		[NativeName("numberOfInProgressRequests")]
		public int NumberOfInProgressRequests { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004119 File Offset: 0x00002319
		[NativeName("numberOfCompletedRequests")]
		public int NumberOfCompletedRequests { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00004121 File Offset: 0x00002321
		[NativeName("numberOfFailedRequests")]
		public int NumberOfFailedRequests { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00004129 File Offset: 0x00002329
		[NativeName("numberOfWaitingRequests")]
		public int NumberOfWaitingRequests { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004131 File Offset: 0x00002331
		[NativeName("numberOfCanceledRequests")]
		public int NumberOfCanceledRequests { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004139 File Offset: 0x00002339
		[NativeName("totalNumberOfRequests")]
		public int TotalNumberOfRequests { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00004141 File Offset: 0x00002341
		[NativeName("numberOfCachedReads")]
		public int NumberOfCachedReads { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004149 File Offset: 0x00002349
		[NativeName("numberOfAsyncReads")]
		public int NumberOfAsyncReads { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004151 File Offset: 0x00002351
		[NativeName("numberOfSyncReads")]
		public int NumberOfSyncReads { get; }
	}
}
