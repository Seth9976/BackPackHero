using System;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x0200016D RID: 365
	[BurstCompile]
	public struct JobMemSet<[IsUnmanaged] T> : IJob where T : struct, ValueType
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x0003BAB5 File Offset: 0x00039CB5
		public void Execute()
		{
			this.data.AsUnsafeSpan<T>().Fill(this.value);
		}

		// Token: 0x04000713 RID: 1811
		[WriteOnly]
		public NativeArray<T> data;

		// Token: 0x04000714 RID: 1812
		public T value;
	}
}
