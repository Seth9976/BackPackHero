using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000066 RID: 102
	internal class DecalDrawCallChunk : DecalChunk
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000154DB File Offset: 0x000136DB
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x000154CC File Offset: 0x000136CC
		public int subCallCount
		{
			get
			{
				return this.subCallCounts[0];
			}
			set
			{
				this.subCallCounts[0] = value;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000154EC File Offset: 0x000136EC
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<float4x4>(ref this.decalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.normalToDecals, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalSubDrawCall>(ref this.subCalls, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00015542 File Offset: 0x00013742
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalToWorlds).ResizeArray(newCapacity);
			(ref this.normalToDecals).ResizeArray(newCapacity);
			(ref this.subCalls).ResizeArray(newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00015570 File Offset: 0x00013770
		public override void Dispose()
		{
			this.subCallCounts.Dispose();
			if (base.capacity == 0)
			{
				return;
			}
			this.decalToWorlds.Dispose();
			this.normalToDecals.Dispose();
			this.subCalls.Dispose();
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x040002A3 RID: 675
		public NativeArray<float4x4> decalToWorlds;

		// Token: 0x040002A4 RID: 676
		public NativeArray<float4x4> normalToDecals;

		// Token: 0x040002A5 RID: 677
		public NativeArray<DecalSubDrawCall> subCalls;

		// Token: 0x040002A6 RID: 678
		public NativeArray<int> subCallCounts;
	}
}
