using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	// Token: 0x0200042C RID: 1068
	[NativeConditional("ENABLE_PROFILER")]
	[StaticAccessor("FileAccessor", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	internal static class File
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
		internal static ulong totalOpenCalls
		{
			get
			{
				return File.GetTotalOpenCalls();
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
		internal static ulong totalCloseCalls
		{
			get
			{
				return File.GetTotalCloseCalls();
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		internal static ulong totalReadCalls
		{
			get
			{
				return File.GetTotalReadCalls();
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0003ED10 File Offset: 0x0003CF10
		internal static ulong totalWriteCalls
		{
			get
			{
				return File.GetTotalWriteCalls();
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x0003ED28 File Offset: 0x0003CF28
		internal static ulong totalSeekCalls
		{
			get
			{
				return File.GetTotalSeekCalls();
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0003ED40 File Offset: 0x0003CF40
		internal static ulong totalZeroSeekCalls
		{
			get
			{
				return File.GetTotalZeroSeekCalls();
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x0003ED58 File Offset: 0x0003CF58
		internal static ulong totalFilesOpened
		{
			get
			{
				return File.GetTotalFilesOpened();
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x0003ED70 File Offset: 0x0003CF70
		internal static ulong totalFilesClosed
		{
			get
			{
				return File.GetTotalFilesClosed();
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x0003ED88 File Offset: 0x0003CF88
		internal static ulong totalBytesRead
		{
			get
			{
				return File.GetTotalBytesRead();
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
		internal static ulong totalBytesWritten
		{
			get
			{
				return File.GetTotalBytesWritten();
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x0003EDB7 File Offset: 0x0003CFB7
		internal static bool recordZeroSeeks
		{
			get
			{
				return File.GetRecordZeroSeeks();
			}
			set
			{
				File.SetRecordZeroSeeks(value);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x0003EDDC File Offset: 0x0003CFDC
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x0003EDF3 File Offset: 0x0003CFF3
		internal static ThreadIORestrictionMode MainThreadIORestrictionMode
		{
			get
			{
				return File.GetMainThreadFileIORestriction();
			}
			set
			{
				File.SetMainThreadFileIORestriction(value);
			}
		}

		// Token: 0x0600253A RID: 9530
		[MethodImpl(4096)]
		internal static extern void SetRecordZeroSeeks(bool enable);

		// Token: 0x0600253B RID: 9531
		[MethodImpl(4096)]
		internal static extern bool GetRecordZeroSeeks();

		// Token: 0x0600253C RID: 9532
		[MethodImpl(4096)]
		internal static extern ulong GetTotalOpenCalls();

		// Token: 0x0600253D RID: 9533
		[MethodImpl(4096)]
		internal static extern ulong GetTotalCloseCalls();

		// Token: 0x0600253E RID: 9534
		[MethodImpl(4096)]
		internal static extern ulong GetTotalReadCalls();

		// Token: 0x0600253F RID: 9535
		[MethodImpl(4096)]
		internal static extern ulong GetTotalWriteCalls();

		// Token: 0x06002540 RID: 9536
		[MethodImpl(4096)]
		internal static extern ulong GetTotalSeekCalls();

		// Token: 0x06002541 RID: 9537
		[MethodImpl(4096)]
		internal static extern ulong GetTotalZeroSeekCalls();

		// Token: 0x06002542 RID: 9538
		[MethodImpl(4096)]
		internal static extern ulong GetTotalFilesOpened();

		// Token: 0x06002543 RID: 9539
		[MethodImpl(4096)]
		internal static extern ulong GetTotalFilesClosed();

		// Token: 0x06002544 RID: 9540
		[MethodImpl(4096)]
		internal static extern ulong GetTotalBytesRead();

		// Token: 0x06002545 RID: 9541
		[MethodImpl(4096)]
		internal static extern ulong GetTotalBytesWritten();

		// Token: 0x06002546 RID: 9542
		[MethodImpl(4096)]
		private static extern void SetMainThreadFileIORestriction(ThreadIORestrictionMode mode);

		// Token: 0x06002547 RID: 9543
		[MethodImpl(4096)]
		private static extern ThreadIORestrictionMode GetMainThreadFileIORestriction();
	}
}
