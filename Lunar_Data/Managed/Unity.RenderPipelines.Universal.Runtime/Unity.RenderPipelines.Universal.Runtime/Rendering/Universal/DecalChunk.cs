using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000064 RID: 100
	internal abstract class DecalChunk : IDisposable
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000153F0 File Offset: 0x000135F0
		// (set) Token: 0x0600039A RID: 922 RVA: 0x000153F8 File Offset: 0x000135F8
		public int count { get; protected set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00015401 File Offset: 0x00013601
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00015409 File Offset: 0x00013609
		public int capacity { get; protected set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00015412 File Offset: 0x00013612
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0001541A File Offset: 0x0001361A
		public JobHandle currentJobHandle { get; set; }

		// Token: 0x0600039F RID: 927 RVA: 0x00015424 File Offset: 0x00013624
		public virtual void Push()
		{
			int count = this.count;
			this.count = count + 1;
		}

		// Token: 0x060003A0 RID: 928
		public abstract void RemoveAtSwapBack(int index);

		// Token: 0x060003A1 RID: 929
		public abstract void SetCapacity(int capacity);

		// Token: 0x060003A2 RID: 930 RVA: 0x00015441 File Offset: 0x00013641
		public virtual void Dispose()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00015444 File Offset: 0x00013644
		protected void ResizeNativeArray(ref TransformAccessArray array, DecalProjector[] decalProjectors, int capacity)
		{
			TransformAccessArray transformAccessArray = new TransformAccessArray(capacity, -1);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					transformAccessArray.Add(decalProjectors[i].transform);
				}
				array.Dispose();
			}
			array = transformAccessArray;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001548F File Offset: 0x0001368F
		protected void RemoveAtSwapBack<T>(ref NativeArray<T> array, int index, int count) where T : struct
		{
			array[index] = array[count - 1];
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000154A1 File Offset: 0x000136A1
		protected void RemoveAtSwapBack<T>(ref T[] array, int index, int count)
		{
			array[index] = array[count - 1];
		}
	}
}
