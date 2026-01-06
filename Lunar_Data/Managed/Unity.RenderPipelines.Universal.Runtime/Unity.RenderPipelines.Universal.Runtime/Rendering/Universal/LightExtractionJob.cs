using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BF RID: 191
	[BurstCompile]
	internal struct LightExtractionJob : IJobFor
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001FF50 File Offset: 0x0001E150
		public void Execute(int index)
		{
			VisibleLight visibleLight = this.lights[index];
			float4x4 float4x = visibleLight.localToWorldMatrix;
			this.lightTypes[index] = visibleLight.lightType;
			this.radiuses[index] = visibleLight.range;
			this.directions[index] = float4x.c2.xyz;
			this.positions[index] = float4x.c3.xyz;
			this.coneRadiuses[index] = math.tan(math.radians(visibleLight.spotAngle * 0.5f)) * visibleLight.range;
		}

		// Token: 0x04000483 RID: 1155
		[ReadOnly]
		public NativeArray<VisibleLight> lights;

		// Token: 0x04000484 RID: 1156
		public NativeArray<LightType> lightTypes;

		// Token: 0x04000485 RID: 1157
		public NativeArray<float> radiuses;

		// Token: 0x04000486 RID: 1158
		public NativeArray<float3> directions;

		// Token: 0x04000487 RID: 1159
		public NativeArray<float3> positions;

		// Token: 0x04000488 RID: 1160
		public NativeArray<float> coneRadiuses;
	}
}
