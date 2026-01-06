using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006E RID: 110
	internal class DecalCachedChunk : DecalChunk
	{
		// Token: 0x060003DB RID: 987 RVA: 0x00016EF4 File Offset: 0x000150F4
		public override void RemoveAtSwapBack(int entityIndex)
		{
			base.RemoveAtSwapBack<float4x4>(ref this.decalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.normalToWorlds, entityIndex, base.count);
			base.RemoveAtSwapBack<float4x4>(ref this.sizeOffsets, entityIndex, base.count);
			base.RemoveAtSwapBack<float2>(ref this.drawDistances, entityIndex, base.count);
			base.RemoveAtSwapBack<float2>(ref this.angleFades, entityIndex, base.count);
			base.RemoveAtSwapBack<float4>(ref this.uvScaleBias, entityIndex, base.count);
			base.RemoveAtSwapBack<int>(ref this.layerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<ulong>(ref this.sceneLayerMasks, entityIndex, base.count);
			base.RemoveAtSwapBack<float>(ref this.fadeFactors, entityIndex, base.count);
			base.RemoveAtSwapBack<BoundingSphere>(ref this.boundingSphereArray, entityIndex, base.count);
			base.RemoveAtSwapBack<BoundingSphere>(ref this.boundingSpheres, entityIndex, base.count);
			base.RemoveAtSwapBack<DecalScaleMode>(ref this.scaleModes, entityIndex, base.count);
			base.RemoveAtSwapBack<float3>(ref this.positions, entityIndex, base.count);
			base.RemoveAtSwapBack<quaternion>(ref this.rotation, entityIndex, base.count);
			base.RemoveAtSwapBack<float3>(ref this.scales, entityIndex, base.count);
			base.RemoveAtSwapBack<bool>(ref this.dirty, entityIndex, base.count);
			int count = base.count;
			base.count = count - 1;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00017044 File Offset: 0x00015244
		public override void SetCapacity(int newCapacity)
		{
			(ref this.decalToWorlds).ResizeArray(newCapacity);
			(ref this.normalToWorlds).ResizeArray(newCapacity);
			(ref this.sizeOffsets).ResizeArray(newCapacity);
			(ref this.drawDistances).ResizeArray(newCapacity);
			(ref this.angleFades).ResizeArray(newCapacity);
			(ref this.uvScaleBias).ResizeArray(newCapacity);
			(ref this.layerMasks).ResizeArray(newCapacity);
			(ref this.sceneLayerMasks).ResizeArray(newCapacity);
			(ref this.fadeFactors).ResizeArray(newCapacity);
			(ref this.boundingSpheres).ResizeArray(newCapacity);
			(ref this.scaleModes).ResizeArray(newCapacity);
			(ref this.positions).ResizeArray(newCapacity);
			(ref this.rotation).ResizeArray(newCapacity);
			(ref this.scales).ResizeArray(newCapacity);
			(ref this.dirty).ResizeArray(newCapacity);
			ArrayExtensions.ResizeArray<BoundingSphere>(ref this.boundingSphereArray, newCapacity);
			base.capacity = newCapacity;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00017118 File Offset: 0x00015318
		public override void Dispose()
		{
			if (base.capacity == 0)
			{
				return;
			}
			this.decalToWorlds.Dispose();
			this.normalToWorlds.Dispose();
			this.sizeOffsets.Dispose();
			this.drawDistances.Dispose();
			this.angleFades.Dispose();
			this.uvScaleBias.Dispose();
			this.layerMasks.Dispose();
			this.sceneLayerMasks.Dispose();
			this.fadeFactors.Dispose();
			this.boundingSpheres.Dispose();
			this.scaleModes.Dispose();
			this.positions.Dispose();
			this.rotation.Dispose();
			this.scales.Dispose();
			this.dirty.Dispose();
			base.count = 0;
			base.capacity = 0;
		}

		// Token: 0x040002C8 RID: 712
		public MaterialPropertyBlock propertyBlock;

		// Token: 0x040002C9 RID: 713
		public int passIndexDBuffer;

		// Token: 0x040002CA RID: 714
		public int passIndexEmissive;

		// Token: 0x040002CB RID: 715
		public int passIndexScreenSpace;

		// Token: 0x040002CC RID: 716
		public int passIndexGBuffer;

		// Token: 0x040002CD RID: 717
		public int drawOrder;

		// Token: 0x040002CE RID: 718
		public bool isCreated;

		// Token: 0x040002CF RID: 719
		public NativeArray<float4x4> decalToWorlds;

		// Token: 0x040002D0 RID: 720
		public NativeArray<float4x4> normalToWorlds;

		// Token: 0x040002D1 RID: 721
		public NativeArray<float4x4> sizeOffsets;

		// Token: 0x040002D2 RID: 722
		public NativeArray<float2> drawDistances;

		// Token: 0x040002D3 RID: 723
		public NativeArray<float2> angleFades;

		// Token: 0x040002D4 RID: 724
		public NativeArray<float4> uvScaleBias;

		// Token: 0x040002D5 RID: 725
		public NativeArray<int> layerMasks;

		// Token: 0x040002D6 RID: 726
		public NativeArray<ulong> sceneLayerMasks;

		// Token: 0x040002D7 RID: 727
		public NativeArray<float> fadeFactors;

		// Token: 0x040002D8 RID: 728
		public NativeArray<BoundingSphere> boundingSpheres;

		// Token: 0x040002D9 RID: 729
		public NativeArray<DecalScaleMode> scaleModes;

		// Token: 0x040002DA RID: 730
		public NativeArray<float3> positions;

		// Token: 0x040002DB RID: 731
		public NativeArray<quaternion> rotation;

		// Token: 0x040002DC RID: 732
		public NativeArray<float3> scales;

		// Token: 0x040002DD RID: 733
		public NativeArray<bool> dirty;

		// Token: 0x040002DE RID: 734
		public BoundingSphere[] boundingSphereArray;
	}
}
