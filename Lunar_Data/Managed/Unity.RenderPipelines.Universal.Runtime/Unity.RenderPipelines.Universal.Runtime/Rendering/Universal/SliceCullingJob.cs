using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C5 RID: 197
	[BurstCompile]
	internal struct SliceCullingJob : IJobFor
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00020490 File Offset: 0x0001E690
		public void Execute(int index)
		{
			float num = (float)index * this.scale * 2f - 1f;
			float num2 = ((float)index + 1f) * this.scale * 2f - 1f;
			SliceCullingJob.Plane plane = SliceCullingJob.ComputePlane(this.viewOrigin, this.viewOrigin + this.viewForward + this.viewRight * num + this.viewUp, this.viewOrigin + this.viewForward + this.viewRight * num - this.viewUp);
			SliceCullingJob.Plane plane2 = SliceCullingJob.ComputePlane(this.viewOrigin, this.viewOrigin + this.viewForward + this.viewRight * num2 - this.viewUp, this.viewOrigin + this.viewForward + this.viewRight * num2 + this.viewUp);
			int length = this.lightTypes.Length;
			int num3 = (length + 31) / 32;
			int num4 = index * this.lightsPerTile / 32;
			for (int i = 0; i < num3; i++)
			{
				uint num5 = 0U;
				int num6 = math.min(32, length - i * 32);
				for (int j = 0; j < num6; j++)
				{
					int num7 = i * 32 + j;
					if (this.ContainsLight(plane, plane2, num7))
					{
						num5 |= 1U << j;
					}
				}
				int num8 = num4 + i;
				this.sliceLightMasks[num8] = this.sliceLightMasks[num8] | num5;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002063C File Offset: 0x0001E83C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool ContainsLight(SliceCullingJob.Plane leftPlane, SliceCullingJob.Plane rightPlane, int lightIndex)
		{
			bool flag = true;
			SliceCullingJob.Sphere sphere = new SliceCullingJob.Sphere
			{
				center = this.positions[lightIndex],
				radius = this.radiuses[lightIndex]
			};
			if (SliceCullingJob.SphereBehindPlane(sphere, leftPlane) || SliceCullingJob.SphereBehindPlane(sphere, rightPlane))
			{
				flag = false;
			}
			if (flag && this.lightTypes[lightIndex] == LightType.Spot)
			{
				SliceCullingJob.Cone cone = new SliceCullingJob.Cone
				{
					tip = sphere.center,
					direction = this.directions[lightIndex],
					height = this.radiuses[lightIndex],
					radius = this.coneRadiuses[lightIndex]
				};
				if (SliceCullingJob.ConeBehindPlane(cone, leftPlane) || SliceCullingJob.ConeBehindPlane(cone, rightPlane))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00020708 File Offset: 0x0001E908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static SliceCullingJob.Plane ComputePlane(float3 p0, float3 p1, float3 p2)
		{
			float3 @float = p1 - p0;
			float3 float2 = p2 - p0;
			SliceCullingJob.Plane plane;
			plane.normal = math.normalize(math.cross(@float, float2));
			plane.distanceToOrigin = math.dot(plane.normal, p0);
			return plane;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002074C File Offset: 0x0001E94C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool SphereBehindPlane(SliceCullingJob.Sphere sphere, SliceCullingJob.Plane plane)
		{
			return math.dot(sphere.center, plane.normal) - plane.distanceToOrigin < -sphere.radius;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002076F File Offset: 0x0001E96F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool PointBehindPlane(float3 p, SliceCullingJob.Plane plane)
		{
			return math.dot(plane.normal, p) - plane.distanceToOrigin < 0f;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002078C File Offset: 0x0001E98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ConeBehindPlane(SliceCullingJob.Cone cone, SliceCullingJob.Plane plane)
		{
			float3 @float = math.cross(math.cross(plane.normal, cone.direction), cone.direction);
			float3 float2 = cone.tip + cone.direction * cone.height - @float * cone.radius;
			return SliceCullingJob.PointBehindPlane(cone.tip, plane) && SliceCullingJob.PointBehindPlane(float2, plane);
		}

		// Token: 0x04000499 RID: 1177
		public float scale;

		// Token: 0x0400049A RID: 1178
		public float3 viewOrigin;

		// Token: 0x0400049B RID: 1179
		public float3 viewForward;

		// Token: 0x0400049C RID: 1180
		public float3 viewRight;

		// Token: 0x0400049D RID: 1181
		public float3 viewUp;

		// Token: 0x0400049E RID: 1182
		[ReadOnly]
		public NativeArray<LightType> lightTypes;

		// Token: 0x0400049F RID: 1183
		[ReadOnly]
		public NativeArray<float> radiuses;

		// Token: 0x040004A0 RID: 1184
		[ReadOnly]
		public NativeArray<float3> directions;

		// Token: 0x040004A1 RID: 1185
		[ReadOnly]
		public NativeArray<float3> positions;

		// Token: 0x040004A2 RID: 1186
		[ReadOnly]
		public NativeArray<float> coneRadiuses;

		// Token: 0x040004A3 RID: 1187
		public int lightsPerTile;

		// Token: 0x040004A4 RID: 1188
		[NativeDisableParallelForRestriction]
		public NativeArray<uint> sliceLightMasks;

		// Token: 0x02000184 RID: 388
		private struct Cone
		{
			// Token: 0x040009E4 RID: 2532
			public float3 tip;

			// Token: 0x040009E5 RID: 2533
			public float3 direction;

			// Token: 0x040009E6 RID: 2534
			public float height;

			// Token: 0x040009E7 RID: 2535
			public float radius;
		}

		// Token: 0x02000185 RID: 389
		private struct Sphere
		{
			// Token: 0x040009E8 RID: 2536
			public float3 center;

			// Token: 0x040009E9 RID: 2537
			public float radius;
		}

		// Token: 0x02000186 RID: 390
		private struct Plane
		{
			// Token: 0x040009EA RID: 2538
			public float3 normal;

			// Token: 0x040009EB RID: 2539
			public float distanceToOrigin;
		}
	}
}
