using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x02000124 RID: 292
	[BurstCompile]
	internal static class NavmeshCutJobs
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x0003078B File Offset: 0x0002E98B
		[BurstCompile(FloatPrecision.Standard, FloatMode.Fast)]
		[MonoPInvokeCallback(typeof(NavmeshCutJobs.CalculateContourDelegate))]
		public unsafe static void CalculateContour(NavmeshCutJobs.JobCalculateContour* job)
		{
			NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Invoke(job);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00030793 File Offset: 0x0002E993
		private static float ApproximateCircleWithPolylineRadius(float radius, int resolution)
		{
			return radius / (1f - (1f - math.cos(3.1415927f / (float)resolution)) * 0.5f);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000307B8 File Offset: 0x0002E9B8
		public unsafe static void CapsuleConvexHullXZ(float4x4 matrix, UnsafeList<float2>* points, float height, float radius, float radiusMargin, int circleResolution, out int numPoints, out float minY, out float maxY)
		{
			height = math.max(height, radius * 2f);
			int num = circleResolution / 2;
			radius = NavmeshCutJobs.ApproximateCircleWithPolylineRadius(radius, num * 2);
			float num2 = math.length(matrix.c0.xyz);
			float num3 = math.length(matrix.c2.xyz);
			radius *= math.max(num2, num3);
			float3 @float = math.normalizesafe(matrix.c1.xyz, default(float3));
			float3 float2 = math.transform(matrix, new float3(0f, -height * 0.5f, 0f)) + @float * radius;
			float3 float3 = math.transform(matrix, new float3(0f, height * 0.5f, 0f)) - @float * radius;
			float2 xz = float2.xz;
			float2 xz2 = float3.xz;
			bool flag = false;
			float2 float4;
			if (math.lengthsq(xz - xz2) < 0.005f)
			{
				float4 = new float2(1f, 0f);
				flag = true;
			}
			else
			{
				float4 = math.normalize(xz2 - xz);
			}
			float2 float5 = new float2(-float4.y, float4.x);
			radius += radiusMargin;
			float4 *= radius;
			float5 *= radius;
			minY = math.min(float2.y, float3.y) - radius;
			maxY = math.max(float2.y, float3.y) + radius;
			float num4 = 3.1415927f / (float)num;
			if (flag)
			{
				numPoints = num * 2;
				int length = points->Length;
				points->Resize(points->Length + numPoints, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < num; i++)
				{
					float num5;
					float num6;
					math.sincos((float)i * num4, out num5, out num6);
					float2 float6 = num5 * float4 + num6 * float5;
					float2 float7 = xz - float6;
					float2 float8 = xz2 + float6;
					*points->ElementAt(length + i) = float7;
					*points->ElementAt(length + i + num) = float8;
				}
				return;
			}
			numPoints = (num + 1) * 2;
			int length2 = points->Length;
			points->Resize(points->Length + numPoints, NativeArrayOptions.UninitializedMemory);
			for (int j = 0; j < num + 1; j++)
			{
				float num7;
				float num8;
				math.sincos((float)j * num4, out num7, out num8);
				float2 float9 = num7 * float4 + num8 * float5;
				float2 float10 = xz - float9;
				float2 float11 = xz2 + float9;
				*points->ElementAt(length2 + j) = float10;
				*points->ElementAt(length2 + j + num + 1) = float11;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00030A74 File Offset: 0x0002EC74
		public unsafe static void BoxConvexHullXZ(float4x4 matrix, UnsafeList<float2>* points, out int numPoints, out float minY, out float maxY)
		{
			minY = float.PositiveInfinity;
			maxY = float.NegativeInfinity;
			int length = points->Length;
			points->Resize(points->Length + NavmeshCutJobs.BoxCorners.Length, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < NavmeshCutJobs.BoxCorners.Length; i++)
			{
				float4 @float = math.mul(matrix, NavmeshCutJobs.BoxCorners[i]);
				minY = math.min(minY, @float.y);
				maxY = math.max(maxY, @float.y);
				*points->ElementAt(length + i) = @float.xz;
			}
			numPoints = NavmeshCutJobs.ConvexHull(points->Ptr + length, NavmeshCutJobs.BoxCorners.Length, 0.01f);
			points->Length = length + numPoints;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00030B34 File Offset: 0x0002ED34
		public unsafe static int ConvexHull(float2* points, int nPoints, float vertexMergeDistance)
		{
			int num = 0;
			for (int i = 0; i < nPoints; i++)
			{
				if (points[i].x < points[num].x || (points[i].x == points[num].x && points[i].y < points[num].y))
				{
					num = i;
				}
			}
			NativeSortExtension.Sort<float2, NavmeshCutJobs.AngleComparator>(points, nPoints, new NavmeshCutJobs.AngleComparator
			{
				origin = points[num]
			});
			int j = 0;
			for (int k = 0; k < nPoints; k++)
			{
				float2 @float = points[k];
				while (j >= 2)
				{
					float2 float2 = points[j - 1] - @float;
					float2 float3 = points[j - 2] - @float;
					if (float2.x * float3.y - float2.y * float3.x < 0f && math.lengthsq(float2) >= vertexMergeDistance)
					{
						break;
					}
					j--;
				}
				if (j == 1 && math.lengthsq(points[j - 1] - @float) < vertexMergeDistance)
				{
					j--;
				}
				points[j] = @float;
				j++;
			}
			return j;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		[BurstCompile(FloatPrecision.Standard, FloatMode.Fast)]
		[MonoPInvokeCallback(typeof(NavmeshCutJobs.CalculateContourDelegate))]
		[MethodImpl(256)]
		public unsafe static void CalculateContour$BurstManaged(NavmeshCutJobs.JobCalculateContour* job)
		{
			job->Execute();
		}

		// Token: 0x04000628 RID: 1576
		private static readonly float4[] BoxCorners = new float4[]
		{
			new float4(-0.5f, -0.5f, -0.5f, 1f),
			new float4(0.5f, -0.5f, -0.5f, 1f),
			new float4(-0.5f, 0.5f, -0.5f, 1f),
			new float4(0.5f, 0.5f, -0.5f, 1f),
			new float4(-0.5f, -0.5f, 0.5f, 1f),
			new float4(0.5f, -0.5f, 0.5f, 1f),
			new float4(-0.5f, 0.5f, 0.5f, 1f),
			new float4(0.5f, 0.5f, 0.5f, 1f)
		};

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x060008EB RID: 2283
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void CalculateContourDelegate(NavmeshCutJobs.JobCalculateContour* job);

		// Token: 0x02000126 RID: 294
		public struct JobCalculateContour
		{
			// Token: 0x060008EE RID: 2286 RVA: 0x00030DDC File Offset: 0x0002EFDC
			public unsafe void Execute()
			{
				this.circleResolution = math.max(this.circleResolution, 3);
				float4x4 float4x = math.mul(this.matrix, this.localToWorldMatrix);
				float num = math.length(float4x.c0);
				float num2 = math.length(float4x.c1);
				float num3 = math.length(float4x.c2);
				switch (this.meshType)
				{
				case NavmeshCut.MeshType.Rectangle:
				{
					this.rectangleSize = new float2(math.abs(this.rectangleSize.x), math.abs(this.rectangleSize.y)) + math.rcp(new float2(num, num3)) * this.radiusMargin * 2f;
					ref UnsafeList<float2> ptr = ref *this.outputVertices;
					float2 @float = math.transform(float4x, new float3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f).xz;
					ptr.Add(in @float);
					ref UnsafeList<float2> ptr2 = ref *this.outputVertices;
					@float = math.transform(float4x, new float3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f).xz;
					ptr2.Add(in @float);
					ref UnsafeList<float2> ptr3 = ref *this.outputVertices;
					@float = math.transform(float4x, new float3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f).xz;
					ptr3.Add(in @float);
					ref UnsafeList<float2> ptr4 = ref *this.outputVertices;
					@float = math.transform(float4x, new float3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f).xz;
					ptr4.Add(in @float);
					float y = float4x.c3.y;
					ref UnsafeList<NavmeshCut.ContourBurst> ptr5 = ref *this.outputContours;
					NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
					contourBurst.ymin = y - this.height * 0.5f * num2;
					contourBurst.ymax = y + this.height * 0.5f * num2;
					contourBurst.startIndex = this.outputVertices->Length - 4;
					contourBurst.endIndex = this.outputVertices->Length;
					ptr5.Add(in contourBurst);
					break;
				}
				case NavmeshCut.MeshType.Circle:
				{
					this.circleRadius = math.abs(this.circleRadius);
					float num4 = this.height + this.radiusMargin / num2;
					float num5 = this.circleRadius + this.radiusMargin / num;
					float num6 = this.circleRadius + this.radiusMargin / num3;
					float num7 = 6.2831855f / (float)this.circleResolution;
					for (int i = 0; i < this.circleResolution; i++)
					{
						float num8;
						float num9;
						math.sincos((float)i * num7, out num8, out num9);
						ref UnsafeList<float2> ptr6 = ref *this.outputVertices;
						float2 @float = math.transform(float4x, new float3(num9 * num5, 0f, num8 * num6)).xz;
						ptr6.Add(in @float);
					}
					float y2 = float4x.c3.y;
					ref UnsafeList<NavmeshCut.ContourBurst> ptr7 = ref *this.outputContours;
					NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
					contourBurst.ymin = y2 - num4 * 0.5f * num2;
					contourBurst.ymax = y2 + num4 * 0.5f * num2;
					contourBurst.startIndex = this.outputVertices->Length - this.circleResolution;
					contourBurst.endIndex = this.outputVertices->Length;
					ptr7.Add(in contourBurst);
					break;
				}
				case NavmeshCut.MeshType.CustomMesh:
					if (this.meshContours != null && this.meshContourVertices != null && this.meshScale > 0f)
					{
						float4x = math.mul(float4x, float4x4.Scale(new float3(this.meshScale)));
						int length = this.outputVertices->Length;
						for (int j = 0; j < this.meshContourVertices->Length; j++)
						{
							ref UnsafeList<float2> ptr8 = ref *this.outputVertices;
							float2 @float = math.transform(float4x, *this.meshContourVertices->ElementAt(j)).xz;
							ptr8.Add(in @float);
						}
						float y3 = float4x.c3.y;
						for (int k = 0; k < this.meshContours->Length; k++)
						{
							ref UnsafeList<NavmeshCut.ContourBurst> ptr9 = ref *this.outputContours;
							NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
							contourBurst.ymin = y3 - this.height * 0.5f * num2;
							contourBurst.ymax = y3 + this.height * 0.5f * num2;
							contourBurst.startIndex = length + this.meshContours->ElementAt(k).startIndex;
							contourBurst.endIndex = length + this.meshContours->ElementAt(k).endIndex;
							ptr9.Add(in contourBurst);
						}
					}
					break;
				case NavmeshCut.MeshType.Box:
				{
					float3 float2 = new float3(this.rectangleSize.x, this.height, this.rectangleSize.y) + math.rcp(new float3(num, num2, num3)) * this.radiusMargin * 2f;
					float4x = math.mul(float4x, float4x4.Scale(float2));
					int num10;
					float num11;
					float num12;
					NavmeshCutJobs.BoxConvexHullXZ(float4x, this.outputVertices, out num10, out num11, out num12);
					ref UnsafeList<NavmeshCut.ContourBurst> ptr10 = ref *this.outputContours;
					NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
					contourBurst.ymin = num11;
					contourBurst.ymax = num12;
					contourBurst.startIndex = this.outputVertices->Length - num10;
					contourBurst.endIndex = this.outputVertices->Length;
					ptr10.Add(in contourBurst);
					break;
				}
				case NavmeshCut.MeshType.Sphere:
				{
					this.circleRadius = math.abs(this.circleRadius);
					float4x = math.mul(this.matrix, float4x4.Translate(this.localToWorldMatrix.c3.xyz));
					float num13 = math.max(num, math.max(num2, num3));
					float4x = math.mul(float4x, float4x4.Scale(num13));
					float num14 = this.circleRadius + this.radiusMargin / num13;
					num14 = NavmeshCutJobs.ApproximateCircleWithPolylineRadius(num14, this.circleResolution);
					float num15 = 6.2831855f / (float)this.circleResolution;
					for (int l = 0; l < this.circleResolution; l++)
					{
						float num16;
						float num17;
						math.sincos((float)l * num15, out num16, out num17);
						ref UnsafeList<float2> ptr11 = ref *this.outputVertices;
						float2 @float = math.transform(float4x, new float3(num17 * num14, 0f, num16 * num14)).xz;
						ptr11.Add(in @float);
					}
					float y4 = float4x.c3.y;
					ref UnsafeList<NavmeshCut.ContourBurst> ptr12 = ref *this.outputContours;
					NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
					contourBurst.ymin = y4 - num14 * num13;
					contourBurst.ymax = y4 + num14 * num13;
					contourBurst.startIndex = this.outputVertices->Length - this.circleResolution;
					contourBurst.endIndex = this.outputVertices->Length;
					ptr12.Add(in contourBurst);
					break;
				}
				case NavmeshCut.MeshType.Capsule:
				{
					this.circleResolution = math.max(this.circleResolution, 6);
					float num18 = this.circleRadius;
					float num19 = this.height;
					num19 *= num2;
					float4x = math.mul(float4x, float4x4.Scale(new float3(1f, 1f / num2, 1f)));
					int num20;
					float num21;
					float num22;
					NavmeshCutJobs.CapsuleConvexHullXZ(float4x, this.outputVertices, num19, num18, this.radiusMargin, this.circleResolution, out num20, out num21, out num22);
					ref UnsafeList<NavmeshCut.ContourBurst> ptr13 = ref *this.outputContours;
					NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
					contourBurst.ymin = num21;
					contourBurst.ymax = num22;
					contourBurst.startIndex = this.outputVertices->Length - num20;
					contourBurst.endIndex = this.outputVertices->Length;
					ptr13.Add(in contourBurst);
					break;
				}
				}
				for (int m = 0; m < this.outputContours->Length; m++)
				{
					NavmeshCut.ContourBurst contourBurst2 = *this.outputContours->ElementAt(m);
					this.WindCounterClockwise(this.outputVertices, contourBurst2.startIndex, contourBurst2.endIndex);
				}
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x000315C4 File Offset: 0x0002F7C4
			private unsafe void WindCounterClockwise(UnsafeList<float2>* vertices, int startIndex, int endIndex)
			{
				int num = 0;
				float2 @float = new float2(float.PositiveInfinity, float.PositiveInfinity);
				for (int i = startIndex; i < endIndex; i++)
				{
					float2 float2 = *vertices->ElementAt(i);
					if (float2.x < @float.x || (float2.x == @float.x && float2.y < @float.y))
					{
						num = i;
						@float = float2;
					}
				}
				int num2 = endIndex - startIndex;
				float2 float3 = (*vertices)[(num - 1 - startIndex + num2) % num2 + startIndex];
				float2 float4 = @float;
				float2 float5 = (*vertices)[(num + 1 - startIndex) % num2 + startIndex];
				if ((float4.x - float3.x) * (float5.y - float3.y) - (float5.x - float3.x) * (float4.y - float3.y) > 0f)
				{
					int j = startIndex;
					int num3 = endIndex - 1;
					while (j < num3)
					{
						float2 float6 = *vertices->ElementAt(j);
						*vertices->ElementAt(j) = *vertices->ElementAt(num3);
						*vertices->ElementAt(num3) = float6;
						j++;
						num3--;
					}
				}
			}

			// Token: 0x04000629 RID: 1577
			public unsafe UnsafeList<float2>* outputVertices;

			// Token: 0x0400062A RID: 1578
			public unsafe UnsafeList<NavmeshCut.ContourBurst>* outputContours;

			// Token: 0x0400062B RID: 1579
			public unsafe UnsafeList<NavmeshCut.ContourBurst>* meshContours;

			// Token: 0x0400062C RID: 1580
			public unsafe UnsafeList<float3>* meshContourVertices;

			// Token: 0x0400062D RID: 1581
			public float4x4 matrix;

			// Token: 0x0400062E RID: 1582
			public float4x4 localToWorldMatrix;

			// Token: 0x0400062F RID: 1583
			public float radiusMargin;

			// Token: 0x04000630 RID: 1584
			public int circleResolution;

			// Token: 0x04000631 RID: 1585
			public float circleRadius;

			// Token: 0x04000632 RID: 1586
			public float2 rectangleSize;

			// Token: 0x04000633 RID: 1587
			public float height;

			// Token: 0x04000634 RID: 1588
			public float meshScale;

			// Token: 0x04000635 RID: 1589
			public NavmeshCut.MeshType meshType;
		}

		// Token: 0x02000127 RID: 295
		private struct AngleComparator : IComparer<float2>
		{
			// Token: 0x060008F0 RID: 2288 RVA: 0x000316F8 File Offset: 0x0002F8F8
			public int Compare(float2 lhs, float2 rhs)
			{
				float2 @float = lhs - this.origin;
				float2 float2 = rhs - this.origin;
				float num = @float.x * float2.y - @float.y * float2.x;
				if (num == 0f)
				{
					float num2 = math.lengthsq(@float);
					float num3 = math.lengthsq(float2);
					if (num2 < num3)
					{
						return 1;
					}
					if (num3 >= num2)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (num >= 0f)
					{
						return -1;
					}
					return 1;
				}
			}

			// Token: 0x04000636 RID: 1590
			public float2 origin;
		}

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x060008F2 RID: 2290
		public unsafe delegate void CalculateContour_00000875$PostfixBurstDelegate(NavmeshCutJobs.JobCalculateContour* job);

		// Token: 0x02000129 RID: 297
		internal static class CalculateContour_00000875$BurstDirectCall
		{
			// Token: 0x060008F5 RID: 2293 RVA: 0x0003176D File Offset: 0x0002F96D
			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Pointer == 0)
				{
					NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.DeferredCompilation, methodof(NavmeshCutJobs.CalculateContour$BurstManaged(NavmeshCutJobs.JobCalculateContour*)).MethodHandle, typeof(NavmeshCutJobs.CalculateContour_00000875$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Pointer;
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x0003179C File Offset: 0x0002F99C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x060008F7 RID: 2295 RVA: 0x000317B4 File Offset: 0x0002F9B4
			public unsafe static void Constructor()
			{
				NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(NavmeshCutJobs.CalculateContour(NavmeshCutJobs.JobCalculateContour*)).MethodHandle);
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x000317C5 File Offset: 0x0002F9C5
			// Note: this type is marked as 'beforefieldinit'.
			static CalculateContour_00000875$BurstDirectCall()
			{
				NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.Constructor();
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x000317CC File Offset: 0x0002F9CC
			public unsafe static void Invoke(NavmeshCutJobs.JobCalculateContour* job)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = NavmeshCutJobs.CalculateContour_00000875$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.NavmeshCutJobs/JobCalculateContour*), job, functionPointer);
						return;
					}
				}
				NavmeshCutJobs.CalculateContour$BurstManaged(job);
			}

			// Token: 0x04000637 RID: 1591
			private static IntPtr Pointer;

			// Token: 0x04000638 RID: 1592
			private static IntPtr DeferredCompilation;
		}
	}
}
