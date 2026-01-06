using System;
using Unity.Collections;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006B RID: 107
	internal class DecalEntityChunk : DecalChunk
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00016034 File Offset: 0x00014234
		public override void Push()
		{
			int count = base.count;
			base.count = count + 1;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00016054 File Offset: 0x00014254
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<DecalEntity>(ref this.decalEntities, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalProjector>(ref this.decalProjectors, entityIndex, base.count);
			this.transformAccessArray.RemoveAtSwapBack(entityIndex);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000160A3 File Offset: 0x000142A3
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalEntities).ResizeArray(newCapacity);
			base.ResizeNativeArray(ref this.transformAccessArray, this.decalProjectors, newCapacity);
			ArrayExtensions.ResizeArray<DecalProjector>(ref this.decalProjectors, newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000160D7 File Offset: 0x000142D7
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.decalEntities.Dispose();
			this.transformAccessArray.Dispose();
			this.decalProjectors = null;
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x040002B3 RID: 691
		public Material material;

		// Token: 0x040002B4 RID: 692
		public NativeArray<DecalEntity> decalEntities;

		// Token: 0x040002B5 RID: 693
		public DecalProjector[] decalProjectors;

		// Token: 0x040002B6 RID: 694
		public TransformAccessArray transformAccessArray;
	}
}
