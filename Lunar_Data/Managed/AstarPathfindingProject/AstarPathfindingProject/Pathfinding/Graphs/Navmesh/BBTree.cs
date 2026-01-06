using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Drawing;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001A0 RID: 416
	[BurstCompile]
	public struct BBTree : IDisposable
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0003F0B0 File Offset: 0x0003D2B0
		public IntRect Size
		{
			get
			{
				if (this.tree.Length != 0)
				{
					return this.tree[0].rect;
				}
				return default(IntRect);
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0003F0E5 File Offset: 0x0003D2E5
		public void Dispose()
		{
			this.nodePermutation.Dispose();
			this.tree.Dispose();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003F0FD File Offset: 0x0003D2FD
		public BBTree(UnsafeSpan<int> triangles, UnsafeSpan<Int3> vertices)
		{
			if (triangles.Length % 3 != 0)
			{
				throw new ArgumentException("triangles must be a multiple of 3 in length");
			}
			BBTree.Build(ref triangles, ref vertices, out this);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003F11F File Offset: 0x0003D31F
		[BurstCompile]
		private static void Build(ref UnsafeSpan<int> triangles, ref UnsafeSpan<Int3> vertices, out BBTree bbTree)
		{
			BBTree.Build_00000A75$BurstDirectCall.Invoke(ref triangles, ref vertices, out bbTree);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0003F12C File Offset: 0x0003D32C
		private static int SplitByX(NativeArray<IntRect> nodesBounds, NativeArray<int> permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				IntRect intRect = nodesBounds[permutation[i]];
				if ((intRect.xmin + intRect.xmax) / 2 > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003F198 File Offset: 0x0003D398
		private static int SplitByZ(NativeArray<IntRect> nodesBounds, NativeArray<int> permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				IntRect intRect = nodesBounds[permutation[i]];
				if ((intRect.ymin + intRect.ymax) / 2 > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003F204 File Offset: 0x0003D404
		private static int BuildSubtree(NativeArray<int> permutation, NativeArray<IntRect> nodeBounds, ref UnsafeList<int> nodes, ref UnsafeList<BBTree.BBTreeBox> tree, int from, int to, bool odd, int depth)
		{
			IntRect intRect = BBTree.NodeBounds(permutation, nodeBounds, from, to);
			int length = tree.Length;
			BBTree.BBTreeBox bbtreeBox = new BBTree.BBTreeBox(intRect);
			tree.Add(in bbtreeBox);
			if (to - from <= 4)
			{
				if (depth > 26)
				{
					Debug.LogWarning(string.Format("Maximum tree height of {0} exceeded (got depth of {1}). Querying this tree may fail. Is the tree very unbalanced?", 26, depth));
				}
				BBTree.BBTreeBox bbtreeBox2 = tree[length];
				int num = (bbtreeBox2.nodeOffset = nodes.Length);
				tree[length] = bbtreeBox2;
				nodes.Length += 4;
				for (int i = 0; i < 4; i++)
				{
					nodes[num + i] = ((i < to - from) ? permutation[from + i] : (-1));
				}
				return length;
			}
			int num3;
			if (odd)
			{
				int num2 = (intRect.xmin + intRect.xmax) / 2;
				num3 = BBTree.SplitByX(nodeBounds, permutation, from, to, num2);
			}
			else
			{
				int num4 = (intRect.ymin + intRect.ymax) / 2;
				num3 = BBTree.SplitByZ(nodeBounds, permutation, from, to, num4);
			}
			int num5 = (to - from) / 8;
			if (num3 <= from + num5 || num3 >= to - num5)
			{
				if (!odd)
				{
					int num6 = (intRect.xmin + intRect.xmax) / 2;
					num3 = BBTree.SplitByX(nodeBounds, permutation, from, to, num6);
				}
				else
				{
					int num7 = (intRect.ymin + intRect.ymax) / 2;
					num3 = BBTree.SplitByZ(nodeBounds, permutation, from, to, num7);
				}
				if (num3 <= from + num5 || num3 >= to - num5)
				{
					num3 = (from + to) / 2;
				}
			}
			int num8 = BBTree.BuildSubtree(permutation, nodeBounds, ref nodes, ref tree, from, num3, !odd, depth + 1);
			int num9 = BBTree.BuildSubtree(permutation, nodeBounds, ref nodes, ref tree, num3, to, !odd, depth + 1);
			BBTree.BBTreeBox bbtreeBox3 = tree[length];
			bbtreeBox3.left = num8;
			bbtreeBox3.right = num9;
			tree[length] = bbtreeBox3;
			return length;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
		private static IntRect NodeBounds(NativeArray<int> permutation, NativeArray<IntRect> nodeBounds, int from, int to)
		{
			int2 @int = (int2)nodeBounds[permutation[from]].Min;
			int2 int2 = (int2)nodeBounds[permutation[from]].Max;
			for (int i = from + 1; i < to; i++)
			{
				IntRect intRect = nodeBounds[permutation[i]];
				int2 int3 = new int2(intRect.xmin, intRect.ymin);
				int2 int4 = new int2(intRect.xmax, intRect.ymax);
				@int = math.min(@int, int3);
				int2 = math.max(int2, int4);
			}
			return new IntRect(@int.x, @int.y, int2.x, int2.y);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003F4AF File Offset: 0x0003D6AF
		public float DistanceSqrLowerBound(float3 p, in BBTree.ProjectionParams projection)
		{
			if (this.tree.Length == 0)
			{
				return float.PositiveInfinity;
			}
			return projection.SquaredRectPointDistanceOnPlane(this.tree[0].rect, p);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003F4DC File Offset: 0x0003D6DC
		public unsafe void QueryClosest(float3 p, NNConstraint constraint, in BBTree.ProjectionParams projection, ref float distanceSqr, ref NNInfo previous, GraphNode[] nodes, UnsafeSpan<int> triangles, UnsafeSpan<Int3> vertices)
		{
			if (this.tree.Length == 0)
			{
				return;
			}
			checked
			{
				BBTree.NearbyNodesIterator.BoxWithDist* ptr = stackalloc BBTree.NearbyNodesIterator.BoxWithDist[unchecked((UIntPtr)26) * (UIntPtr)sizeof(BBTree.NearbyNodesIterator.BoxWithDist)];
				UnsafeSpan<BBTree.NearbyNodesIterator.BoxWithDist> unsafeSpan = new UnsafeSpan<BBTree.NearbyNodesIterator.BoxWithDist>((void*)ptr, 26);
				*unsafeSpan[0] = new BBTree.NearbyNodesIterator.BoxWithDist
				{
					index = 0,
					distSqr = 0f
				};
				BBTree.NearbyNodesIterator nearbyNodesIterator = new BBTree.NearbyNodesIterator
				{
					stack = unsafeSpan,
					stackSize = 1,
					indexInLeaf = 0,
					point = p,
					projection = projection,
					distanceThresholdSqr = distanceSqr,
					tieBreakingDistanceThreshold = float.PositiveInfinity,
					tree = this.tree.AsUnsafeSpan<BBTree.BBTreeBox>(),
					nodes = this.nodePermutation.AsUnsafeSpan<int>(),
					triangles = triangles,
					vertices = vertices
				};
				NNInfo nninfo = previous;
				while (nearbyNodesIterator.stackSize > 0 && nearbyNodesIterator.MoveNext())
				{
					BBTree.CloseNode current = nearbyNodesIterator.current;
					if (constraint == null || constraint.Suitable(nodes[current.node]))
					{
						nearbyNodesIterator.distanceThresholdSqr = current.distanceSq;
						nearbyNodesIterator.tieBreakingDistanceThreshold = current.tieBreakingDistance;
						nninfo = new NNInfo(nodes[current.node], current.closestPointOnNode, current.distanceSq);
					}
				}
				distanceSqr = nearbyNodesIterator.distanceThresholdSqr;
				previous = nninfo;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003F647 File Offset: 0x0003D847
		public void DrawGizmos(CommandBuilder draw)
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.tree.Length == 0)
			{
				return;
			}
			this.DrawGizmos(ref draw, 0, 0);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0003F680 File Offset: 0x0003D880
		private void DrawGizmos(ref CommandBuilder draw, int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			Vector3 vector = (Vector3)new Int3(bbtreeBox.rect.xmin, 0, bbtreeBox.rect.ymin);
			Vector3 vector2 = (Vector3)new Int3(bbtreeBox.rect.xmax, 0, bbtreeBox.rect.ymax);
			Vector3 vector3 = (vector + vector2) * 0.5f;
			Vector3 vector4 = vector2 - vector;
			vector4 = new Vector3(vector4.x, 1f, vector4.z);
			vector3.y += (float)(depth * 2);
			draw.xz.WireRectangle(vector3, new float2(vector4.x, vector4.z), AstarMath.IntToColor(depth, 1f));
			if (!bbtreeBox.IsLeaf)
			{
				this.DrawGizmos(ref draw, bbtreeBox.left, depth + 1);
				this.DrawGizmos(ref draw, bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003F780 File Offset: 0x0003D980
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static void Build$BurstManaged(ref UnsafeSpan<int> triangles, ref UnsafeSpan<Int3> vertices, out BBTree bbTree)
		{
			int num = triangles.Length / 3;
			UnsafeList<BBTree.BBTreeBox> unsafeList = new UnsafeList<BBTree.BBTreeBox>((int)((float)num * 2.1f), Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			UnsafeList<int> unsafeList2 = new UnsafeList<int>((int)((float)num * 1.1f), Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<int> nativeArray = new NativeArray<int>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < num; i++)
			{
				nativeArray[i] = i;
			}
			NativeArray<IntRect> nativeArray2 = new NativeArray<IntRect>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < num; j++)
			{
				int2 xz = ((int3)(*vertices[*triangles[j * 3]])).xz;
				int2 xz2 = ((int3)(*vertices[*triangles[j * 3 + 1]])).xz;
				int2 xz3 = ((int3)(*vertices[*triangles[j * 3 + 2]])).xz;
				int2 @int = math.min(xz, math.min(xz2, xz3));
				int2 int2 = math.max(xz, math.max(xz2, xz3));
				nativeArray2[j] = new IntRect(@int.x, @int.y, int2.x, int2.y);
			}
			if (num > 0)
			{
				BBTree.BuildSubtree(nativeArray, nativeArray2, ref unsafeList2, ref unsafeList, 0, num, false, 0);
			}
			nativeArray2.Dispose();
			nativeArray.Dispose();
			bbTree = new BBTree
			{
				tree = unsafeList,
				nodePermutation = unsafeList2
			};
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0003F90B File Offset: 0x0003DB0B
		public static void Initialize$NearbyNodesIterator_MoveNext_00000A87$BurstDirectCall()
		{
			BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Initialize();
		}

		// Token: 0x040007AE RID: 1966
		private UnsafeList<BBTree.BBTreeBox> tree;

		// Token: 0x040007AF RID: 1967
		private UnsafeList<int> nodePermutation;

		// Token: 0x040007B0 RID: 1968
		private const int MaximumLeafSize = 4;

		// Token: 0x040007B1 RID: 1969
		private const int MAX_TREE_HEIGHT = 26;

		// Token: 0x020001A1 RID: 417
		[BurstCompile]
		public readonly struct ProjectionParams
		{
			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0003F912 File Offset: 0x0003DB12
			public bool alignedWithXZPlane
			{
				get
				{
					return this.alignedWithXZPlaneBacking > 0;
				}
			}

			// Token: 0x06000B3C RID: 2876 RVA: 0x0003F91D File Offset: 0x0003DB1D
			public float SquaredRectPointDistanceOnPlane(IntRect rect, float3 p)
			{
				return BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane(in this, ref rect, ref p);
			}

			// Token: 0x06000B3D RID: 2877 RVA: 0x0003F929 File Offset: 0x0003DB29
			[BurstCompile(FloatMode = FloatMode.Fast)]
			private static float SquaredRectPointDistanceOnPlane(in BBTree.ProjectionParams projection, ref IntRect rect, ref float3 p)
			{
				return BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Invoke(in projection, ref rect, ref p);
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x0003F934 File Offset: 0x0003DB34
			public ProjectionParams(NNConstraint constraint, GraphTransform graphTransform)
			{
				if (constraint == null || !(constraint.distanceMetric.projectionAxis != Vector3.zero))
				{
					this.projectionAxis = float3.zero;
					this.planeProjection = default(float2x3);
					this.projectedUpNormalized = default(float2);
					this.distanceMetric = BBTree.DistanceMetric.Euclidean;
					this.alignedWithXZPlaneBacking = 1;
					this.distanceScaleAlongProjectionAxis = 0f;
					return;
				}
				if (float.IsPositiveInfinity(constraint.distanceMetric.projectionAxis.x))
				{
					this.projectionAxis = new float3(0f, 1f, 0f);
				}
				else
				{
					this.projectionAxis = math.normalizesafe(graphTransform.InverseTransformVector(constraint.distanceMetric.projectionAxis), default(float3));
				}
				if (this.projectionAxis.x * this.projectionAxis.x + this.projectionAxis.z * this.projectionAxis.z < 0.0001f)
				{
					this.projectedUpNormalized = float2.zero;
					this.planeProjection = new float2x3(1f, 0f, 0f, 0f, 0f, 1f);
					this.distanceMetric = BBTree.DistanceMetric.ScaledManhattan;
					this.alignedWithXZPlaneBacking = 1;
					this.distanceScaleAlongProjectionAxis = math.max(constraint.distanceMetric.distanceScaleAlongProjectionDirection, 0f);
					return;
				}
				float3 @float = math.normalizesafe(math.cross(new float3(1f, 0f, 1f), this.projectionAxis), default(float3));
				if (math.all(@float == 0f))
				{
					@float = math.normalizesafe(math.cross(new float3(-1f, 0f, 1f), this.projectionAxis), default(float3));
				}
				float3 float2 = math.normalizesafe(math.cross(this.projectionAxis, @float), default(float3));
				this.planeProjection = math.transpose(new float3x2(@float, float2));
				this.projectedUpNormalized = ((math.lengthsq(this.planeProjection.c1) <= 0.0001f) ? float2.zero : math.normalize(this.planeProjection.c1));
				this.distanceMetric = BBTree.DistanceMetric.ScaledManhattan;
				this.alignedWithXZPlaneBacking = (math.all(this.projectedUpNormalized == 0f) ? 1 : 0);
				this.distanceScaleAlongProjectionAxis = math.max(constraint.distanceMetric.distanceScaleAlongProjectionDirection, 0f);
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
			[BurstCompile(FloatMode = FloatMode.Fast)]
			[MethodImpl(256)]
			public static float SquaredRectPointDistanceOnPlane$BurstManaged(in BBTree.ProjectionParams projection, ref IntRect rect, ref float3 p)
			{
				if (projection.alignedWithXZPlane)
				{
					float2 @float = new float2((float)rect.xmin, (float)rect.ymin) * 0.001f;
					float2 float2 = new float2((float)rect.xmax, (float)rect.ymax) * 0.001f;
					return math.lengthsq(math.clamp(p.xz, @float, float2) - p.xz);
				}
				float3 float3 = new float3((float)rect.xmin, 0f, (float)rect.ymin) * 0.001f - p;
				float3 float4 = new float3((float)rect.xmax, 0f, (float)rect.ymax) * 0.001f - p;
				float3 float5 = new float3((float)rect.xmin, 0f, (float)rect.ymax) * 0.001f - p;
				float3 float6 = new float3((float)rect.xmax, 0f, (float)rect.ymin) * 0.001f - p;
				float2 float7 = math.mul(projection.planeProjection, float3);
				float2 float8 = math.mul(projection.planeProjection, float5);
				float2 float9 = math.mul(projection.planeProjection, float6);
				float2 float10 = math.mul(projection.planeProjection, float4);
				float2 float11 = new float2(projection.projectedUpNormalized.y, -projection.projectedUpNormalized.x);
				float4 float12 = math.mul(math.transpose(new float2x4(float7, float8, float9, float10)), float11);
				float num = math.clamp(0f, math.cmin(float12), math.cmax(float12));
				return num * num;
			}

			// Token: 0x040007B2 RID: 1970
			public readonly float2x3 planeProjection;

			// Token: 0x040007B3 RID: 1971
			public readonly float2 projectedUpNormalized;

			// Token: 0x040007B4 RID: 1972
			public readonly float3 projectionAxis;

			// Token: 0x040007B5 RID: 1973
			public readonly float distanceScaleAlongProjectionAxis;

			// Token: 0x040007B6 RID: 1974
			public readonly BBTree.DistanceMetric distanceMetric;

			// Token: 0x040007B7 RID: 1975
			private readonly byte alignedWithXZPlaneBacking;

			// Token: 0x020001A2 RID: 418
			// (Invoke) Token: 0x06000B41 RID: 2881
			public delegate float SquaredRectPointDistanceOnPlane_00000A80$PostfixBurstDelegate(in BBTree.ProjectionParams projection, ref IntRect rect, ref float3 p);

			// Token: 0x020001A3 RID: 419
			internal static class SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall
			{
				// Token: 0x06000B44 RID: 2884 RVA: 0x0003FD56 File Offset: 0x0003DF56
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Pointer == 0)
					{
						BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.DeferredCompilation, methodof(BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane$BurstManaged(ref BBTree.ProjectionParams, ref IntRect, ref float3)).MethodHandle, typeof(BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Pointer;
				}

				// Token: 0x06000B45 RID: 2885 RVA: 0x0003FD84 File Offset: 0x0003DF84
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x06000B46 RID: 2886 RVA: 0x0003FD9C File Offset: 0x0003DF9C
				public static void Constructor()
				{
					BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane(ref BBTree.ProjectionParams, ref IntRect, ref float3)).MethodHandle);
				}

				// Token: 0x06000B47 RID: 2887 RVA: 0x000033F6 File Offset: 0x000015F6
				public static void Initialize()
				{
				}

				// Token: 0x06000B48 RID: 2888 RVA: 0x0003FDAD File Offset: 0x0003DFAD
				// Note: this type is marked as 'beforefieldinit'.
				static SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall()
				{
					BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.Constructor();
				}

				// Token: 0x06000B49 RID: 2889 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
				public static float Invoke(in BBTree.ProjectionParams projection, ref IntRect rect, ref float3 p)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane_00000A80$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Single(Pathfinding.Graphs.Navmesh.BBTree/ProjectionParams&,Pathfinding.IntRect&,Unity.Mathematics.float3&), ref projection, ref rect, ref p, functionPointer);
						}
					}
					return BBTree.ProjectionParams.SquaredRectPointDistanceOnPlane$BurstManaged(in projection, ref rect, ref p);
				}

				// Token: 0x040007B8 RID: 1976
				private static IntPtr Pointer;

				// Token: 0x040007B9 RID: 1977
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x020001A4 RID: 420
		private struct CloseNode
		{
			// Token: 0x040007BA RID: 1978
			public int node;

			// Token: 0x040007BB RID: 1979
			public float distanceSq;

			// Token: 0x040007BC RID: 1980
			public float tieBreakingDistance;

			// Token: 0x040007BD RID: 1981
			public float3 closestPointOnNode;
		}

		// Token: 0x020001A5 RID: 421
		public enum DistanceMetric : byte
		{
			// Token: 0x040007BF RID: 1983
			Euclidean,
			// Token: 0x040007C0 RID: 1984
			ScaledManhattan
		}

		// Token: 0x020001A6 RID: 422
		[BurstCompile]
		private struct NearbyNodesIterator : IEnumerator<BBTree.CloseNode>, IEnumerator, IDisposable
		{
			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0003FDE9 File Offset: 0x0003DFE9
			public BBTree.CloseNode Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x0003FDF1 File Offset: 0x0003DFF1
			public bool MoveNext()
			{
				return BBTree.NearbyNodesIterator.MoveNext(ref this);
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x000033F6 File Offset: 0x000015F6
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x000034F2 File Offset: 0x000016F2
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000034F2 File Offset: 0x000016F2
			object IEnumerator.Current
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x0003FDF9 File Offset: 0x0003DFF9
			[BurstCompile(FloatMode = FloatMode.Default)]
			private static bool MoveNext(ref BBTree.NearbyNodesIterator it)
			{
				return BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Invoke(ref it);
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x0003FE04 File Offset: 0x0003E004
			[BurstCompile(FloatMode = FloatMode.Default)]
			[MethodImpl(256)]
			public unsafe static bool MoveNext$BurstManaged(ref BBTree.NearbyNodesIterator it)
			{
				float num = it.distanceThresholdSqr;
				while (it.stackSize != 0)
				{
					BBTree.NearbyNodesIterator.BoxWithDist boxWithDist = *it.stack[it.stackSize - 1];
					if (boxWithDist.distSqr > num)
					{
						it.stackSize--;
						it.indexInLeaf = 0;
					}
					else
					{
						BBTree.BBTreeBox bbtreeBox = *it.tree[boxWithDist.index];
						if (bbtreeBox.IsLeaf)
						{
							for (int i = it.indexInLeaf; i < 4; i++)
							{
								int num2 = *it.nodes[bbtreeBox.nodeOffset + i];
								if (num2 == -1)
								{
									break;
								}
								uint num3 = (uint)(num2 * 3);
								uint num4 = (uint)(num2 * 3 + 1);
								uint num5 = (uint)(num2 * 3 + 2);
								if (num5 >= it.triangles.length)
								{
									throw new Exception("Invalid node index");
								}
								Hint.Assume(num3 < it.triangles.length && num4 < it.triangles.length && num5 < it.triangles.length);
								Int3 @int = *it.vertices[*it.triangles[num3]];
								Int3 int2 = *it.vertices[*it.triangles[num4]];
								Int3 int3 = *it.vertices[*it.triangles[num5]];
								if (it.projection.distanceMetric == BBTree.DistanceMetric.Euclidean)
								{
									float3 @float = (float3)@int;
									float3 float2 = (float3)int2;
									float3 float3 = (float3)int3;
									float3 float4;
									Polygon.ClosestPointOnTriangleByRef(in @float, in float2, in float3, in it.point, out float4);
									float num6 = math.distancesq(float4, it.point);
									if (num6 < num)
									{
										it.indexInLeaf = i + 1;
										it.current = new BBTree.CloseNode
										{
											node = num2,
											distanceSq = num6,
											tieBreakingDistance = 0f,
											closestPointOnNode = float4
										};
										return true;
									}
								}
								else
								{
									float3 float5;
									float num7;
									float num8;
									Polygon.ClosestPointOnTriangleProjected(ref @int, ref int2, ref int3, ref it.projection, ref it.point, out float5, out num7, out num8);
									if (num7 < num || (num7 == num && num8 < it.tieBreakingDistanceThreshold))
									{
										it.indexInLeaf = i + 1;
										it.current = new BBTree.CloseNode
										{
											node = num2,
											distanceSq = num7,
											tieBreakingDistance = num8,
											closestPointOnNode = float5
										};
										return true;
									}
								}
							}
							it.indexInLeaf = 0;
							it.stackSize--;
						}
						else
						{
							it.stackSize--;
							int left = bbtreeBox.left;
							int right = bbtreeBox.right;
							float num9 = it.projection.SquaredRectPointDistanceOnPlane(it.tree[left].rect, it.point);
							float num10 = it.projection.SquaredRectPointDistanceOnPlane(it.tree[right].rect, it.point);
							if (num10 < num9)
							{
								Memory.Swap<int>(ref left, ref right);
								Memory.Swap<float>(ref num9, ref num10);
							}
							if (it.stackSize + 2 > it.stack.Length)
							{
								throw new InvalidOperationException("Tree is too deep. Overflowed the internal stack.");
							}
							if (num10 <= num)
							{
								int num11 = it.stackSize;
								it.stackSize = num11 + 1;
								*it.stack[num11] = new BBTree.NearbyNodesIterator.BoxWithDist
								{
									index = right,
									distSqr = num10
								};
							}
							if (num9 <= num)
							{
								int num11 = it.stackSize;
								it.stackSize = num11 + 1;
								*it.stack[num11] = new BBTree.NearbyNodesIterator.BoxWithDist
								{
									index = left,
									distSqr = num9
								};
							}
						}
					}
				}
				return false;
			}

			// Token: 0x040007C1 RID: 1985
			public UnsafeSpan<BBTree.NearbyNodesIterator.BoxWithDist> stack;

			// Token: 0x040007C2 RID: 1986
			public int stackSize;

			// Token: 0x040007C3 RID: 1987
			public UnsafeSpan<BBTree.BBTreeBox> tree;

			// Token: 0x040007C4 RID: 1988
			public UnsafeSpan<int> nodes;

			// Token: 0x040007C5 RID: 1989
			public UnsafeSpan<int> triangles;

			// Token: 0x040007C6 RID: 1990
			public UnsafeSpan<Int3> vertices;

			// Token: 0x040007C7 RID: 1991
			public int indexInLeaf;

			// Token: 0x040007C8 RID: 1992
			public float3 point;

			// Token: 0x040007C9 RID: 1993
			public BBTree.ProjectionParams projection;

			// Token: 0x040007CA RID: 1994
			public float distanceThresholdSqr;

			// Token: 0x040007CB RID: 1995
			public float tieBreakingDistanceThreshold;

			// Token: 0x040007CC RID: 1996
			internal BBTree.CloseNode current;

			// Token: 0x020001A7 RID: 423
			public struct BoxWithDist
			{
				// Token: 0x040007CD RID: 1997
				public int index;

				// Token: 0x040007CE RID: 1998
				public float distSqr;
			}

			// Token: 0x020001A8 RID: 424
			// (Invoke) Token: 0x06000B52 RID: 2898
			public delegate bool MoveNext_00000A87$PostfixBurstDelegate(ref BBTree.NearbyNodesIterator it);

			// Token: 0x020001A9 RID: 425
			internal static class MoveNext_00000A87$BurstDirectCall
			{
				// Token: 0x06000B55 RID: 2901 RVA: 0x000401BC File Offset: 0x0003E3BC
				[BurstDiscard]
				private static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Pointer == 0)
					{
						BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.DeferredCompilation, methodof(BBTree.NearbyNodesIterator.MoveNext$BurstManaged(ref BBTree.NearbyNodesIterator)).MethodHandle, typeof(BBTree.NearbyNodesIterator.MoveNext_00000A87$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Pointer;
				}

				// Token: 0x06000B56 RID: 2902 RVA: 0x000401E8 File Offset: 0x0003E3E8
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x06000B57 RID: 2903 RVA: 0x00040200 File Offset: 0x0003E400
				public static void Constructor()
				{
					BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BBTree.NearbyNodesIterator.MoveNext(ref BBTree.NearbyNodesIterator)).MethodHandle);
				}

				// Token: 0x06000B58 RID: 2904 RVA: 0x000033F6 File Offset: 0x000015F6
				public static void Initialize()
				{
				}

				// Token: 0x06000B59 RID: 2905 RVA: 0x00040211 File Offset: 0x0003E411
				// Note: this type is marked as 'beforefieldinit'.
				static MoveNext_00000A87$BurstDirectCall()
				{
					BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.Constructor();
				}

				// Token: 0x06000B5A RID: 2906 RVA: 0x00040218 File Offset: 0x0003E418
				public static bool Invoke(ref BBTree.NearbyNodesIterator it)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = BBTree.NearbyNodesIterator.MoveNext_00000A87$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Boolean(Pathfinding.Graphs.Navmesh.BBTree/NearbyNodesIterator&), ref it, functionPointer);
						}
					}
					return BBTree.NearbyNodesIterator.MoveNext$BurstManaged(ref it);
				}

				// Token: 0x040007CF RID: 1999
				private static IntPtr Pointer;

				// Token: 0x040007D0 RID: 2000
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x020001AA RID: 426
		private struct BBTreeBox
		{
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00040249 File Offset: 0x0003E449
			public bool IsLeaf
			{
				get
				{
					return this.nodeOffset >= 0;
				}
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x00040258 File Offset: 0x0003E458
			public BBTreeBox(IntRect rect)
			{
				this.nodeOffset = -1;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x040007D1 RID: 2001
			public IntRect rect;

			// Token: 0x040007D2 RID: 2002
			public int nodeOffset;

			// Token: 0x040007D3 RID: 2003
			public int left;

			// Token: 0x040007D4 RID: 2004
			public int right;
		}

		// Token: 0x020001AB RID: 427
		// (Invoke) Token: 0x06000B5E RID: 2910
		public delegate void Build_00000A75$PostfixBurstDelegate(ref UnsafeSpan<int> triangles, ref UnsafeSpan<Int3> vertices, out BBTree bbTree);

		// Token: 0x020001AC RID: 428
		internal static class Build_00000A75$BurstDirectCall
		{
			// Token: 0x06000B61 RID: 2913 RVA: 0x00040283 File Offset: 0x0003E483
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (BBTree.Build_00000A75$BurstDirectCall.Pointer == 0)
				{
					BBTree.Build_00000A75$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BBTree.Build_00000A75$BurstDirectCall.DeferredCompilation, methodof(BBTree.Build$BurstManaged(ref UnsafeSpan<int>, ref UnsafeSpan<Int3>, ref BBTree)).MethodHandle, typeof(BBTree.Build_00000A75$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = BBTree.Build_00000A75$BurstDirectCall.Pointer;
			}

			// Token: 0x06000B62 RID: 2914 RVA: 0x000402B0 File Offset: 0x0003E4B0
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				BBTree.Build_00000A75$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000B63 RID: 2915 RVA: 0x000402C8 File Offset: 0x0003E4C8
			public static void Constructor()
			{
				BBTree.Build_00000A75$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BBTree.Build(ref UnsafeSpan<int>, ref UnsafeSpan<Int3>, ref BBTree)).MethodHandle);
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x000402D9 File Offset: 0x0003E4D9
			// Note: this type is marked as 'beforefieldinit'.
			static Build_00000A75$BurstDirectCall()
			{
				BBTree.Build_00000A75$BurstDirectCall.Constructor();
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x000402E0 File Offset: 0x0003E4E0
			public static void Invoke(ref UnsafeSpan<int> triangles, ref UnsafeSpan<Int3> vertices, out BBTree bbTree)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = BBTree.Build_00000A75$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Util.UnsafeSpan`1<System.Int32>&,Pathfinding.Util.UnsafeSpan`1<Pathfinding.Int3>&,Pathfinding.Graphs.Navmesh.BBTree&), ref triangles, ref vertices, ref bbTree, functionPointer);
						return;
					}
				}
				BBTree.Build$BurstManaged(ref triangles, ref vertices, out bbTree);
			}

			// Token: 0x040007D5 RID: 2005
			private static IntPtr Pointer;

			// Token: 0x040007D6 RID: 2006
			private static IntPtr DeferredCompilation;
		}
	}
}
