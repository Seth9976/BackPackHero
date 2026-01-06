using System;
using System.Threading;

namespace Hebron.Runtime
{
	// Token: 0x02000003 RID: 3
	internal static class MemoryStats
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002346 File Offset: 0x00000546
		public static int Allocations
		{
			get
			{
				return MemoryStats._allocations;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000234D File Offset: 0x0000054D
		internal static void Allocated()
		{
			Interlocked.Increment(ref MemoryStats._allocations);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000235A File Offset: 0x0000055A
		internal static void Freed()
		{
			Interlocked.Decrement(ref MemoryStats._allocations);
		}

		// Token: 0x04000002 RID: 2
		private static int _allocations;
	}
}
