using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C1 RID: 193
	[BurstCompile]
	internal struct MinMaxZJob : IJobFor
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		public void Execute(int index)
		{
			VisibleLight visibleLight = this.lights[index];
			float4x4 float4x = visibleLight.localToWorldMatrix;
			float3 xyz = float4x.c3.xyz;
			float3 xyz2 = math.mul(this.worldToViewMatrix, math.float4(xyz, 1f)).xyz;
			xyz2.z *= -1f;
			LightMinMaxZ lightMinMaxZ = new LightMinMaxZ
			{
				minZ = xyz2.z - visibleLight.range,
				maxZ = xyz2.z + visibleLight.range
			};
			if (visibleLight.lightType == LightType.Spot)
			{
				float num = math.radians(visibleLight.spotAngle) * 0.5f;
				float num2 = math.cos(num);
				float num3 = visibleLight.range * num2;
				float3 xyz3 = float4x.c2.xyz;
				float3 @float = xyz + xyz3 * num3;
				float3 xyz4 = math.mul(this.worldToViewMatrix, math.float4(@float, 1f)).xyz;
				xyz4.z *= -1f;
				float num4 = 1.5707964f - num;
				float num5 = visibleLight.range * num2 * math.sin(num) / math.sin(num4);
				float3 float2 = xyz4 - xyz2;
				float num6 = math.sqrt(1f - float2.z * float2.z / math.dot(float2, float2));
				if (-float2.z < num3 * num2)
				{
					lightMinMaxZ.minZ = math.min(xyz2.z, xyz4.z - num6 * num5);
				}
				if (float2.z < num3 * num2)
				{
					lightMinMaxZ.maxZ = math.max(xyz2.z, xyz4.z + num6 * num5);
				}
			}
			lightMinMaxZ.minZ = math.max(lightMinMaxZ.minZ, 0f);
			lightMinMaxZ.maxZ = math.max(lightMinMaxZ.maxZ, 0f);
			this.minMaxZs[index] = lightMinMaxZ;
			this.meanZs[index] = (lightMinMaxZ.minZ + lightMinMaxZ.maxZ) / 2f;
		}

		// Token: 0x0400048B RID: 1163
		public float4x4 worldToViewMatrix;

		// Token: 0x0400048C RID: 1164
		[ReadOnly]
		public NativeArray<VisibleLight> lights;

		// Token: 0x0400048D RID: 1165
		public NativeArray<LightMinMaxZ> minMaxZs;

		// Token: 0x0400048E RID: 1166
		public NativeArray<float> meanZs;
	}
}
