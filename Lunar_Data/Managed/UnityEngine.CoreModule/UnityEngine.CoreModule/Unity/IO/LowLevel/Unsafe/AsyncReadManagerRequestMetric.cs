using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000080 RID: 128
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_PROFILER")]
	public struct AsyncReadManagerRequestMetric
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003EE8 File Offset: 0x000020E8
		[NativeName("assetName")]
		public readonly string AssetName { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00003EF0 File Offset: 0x000020F0
		[NativeName("fileName")]
		public readonly string FileName { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003EF8 File Offset: 0x000020F8
		[NativeName("offsetBytes")]
		public readonly ulong OffsetBytes { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003F00 File Offset: 0x00002100
		[NativeName("sizeBytes")]
		public readonly ulong SizeBytes { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00003F08 File Offset: 0x00002108
		[NativeName("assetTypeId")]
		public readonly ulong AssetTypeId { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003F10 File Offset: 0x00002110
		[NativeName("currentBytesRead")]
		public readonly ulong CurrentBytesRead { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003F18 File Offset: 0x00002118
		[NativeName("batchReadCount")]
		public readonly uint BatchReadCount { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00003F20 File Offset: 0x00002120
		[NativeName("isBatchRead")]
		public readonly bool IsBatchRead { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003F28 File Offset: 0x00002128
		[NativeName("state")]
		public readonly ProcessingState State { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00003F30 File Offset: 0x00002130
		[NativeName("readType")]
		public readonly FileReadType ReadType { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003F38 File Offset: 0x00002138
		[NativeName("priorityLevel")]
		public readonly Priority PriorityLevel { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003F40 File Offset: 0x00002140
		[NativeName("subsystem")]
		public readonly AssetLoadingSubsystem Subsystem { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003F48 File Offset: 0x00002148
		[NativeName("requestTimeMicroseconds")]
		public readonly double RequestTimeMicroseconds { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003F50 File Offset: 0x00002150
		[NativeName("timeInQueueMicroseconds")]
		public readonly double TimeInQueueMicroseconds { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00003F58 File Offset: 0x00002158
		[NativeName("totalTimeMicroseconds")]
		public readonly double TotalTimeMicroseconds { get; }
	}
}
