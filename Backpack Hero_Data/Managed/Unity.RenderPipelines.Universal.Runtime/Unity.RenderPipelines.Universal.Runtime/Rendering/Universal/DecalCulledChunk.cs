using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000071 RID: 113
	internal class DecalCulledChunk : DecalChunk
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x00017510 File Offset: 0x00015710
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<int>(ref this.visibleDecalIndexArray, entityIndex, base.count);
			base.RemoveAtSwapBack<int>(ref this.visibleDecalIndices, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00017553 File Offset: 0x00015753
		public override void SetCapacity(int newCapacity)
		{
			ArrayExtensions.ResizeArray<int>(ref this.visibleDecalIndexArray, newCapacity);
			(ref this.visibleDecalIndices).ResizeArray(newCapacity);
			if (this.cullingGroups == null)
			{
				this.cullingGroups = new CullingGroup();
			}
			base.capacity = newCapacity;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00017587 File Offset: 0x00015787
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.visibleDecalIndices.Dispose();
			this.visibleDecalIndexArray = null;
			base.count = 0;
			base.capacity = 0;
			this.cullingGroups.Dispose();
			this.cullingGroups = null;
		}

		// Token: 0x040002E4 RID: 740
		public Vector3 cameraPosition;

		// Token: 0x040002E5 RID: 741
		public ulong sceneCullingMask;

		// Token: 0x040002E6 RID: 742
		public int cullingMask;

		// Token: 0x040002E7 RID: 743
		public CullingGroup cullingGroups;

		// Token: 0x040002E8 RID: 744
		public int[] visibleDecalIndexArray;

		// Token: 0x040002E9 RID: 745
		public NativeArray<int> visibleDecalIndices;

		// Token: 0x040002EA RID: 746
		public int visibleDecalCount;
	}
}
