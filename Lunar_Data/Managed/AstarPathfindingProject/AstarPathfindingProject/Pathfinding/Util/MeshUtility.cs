using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Pathfinding.Util
{
	// Token: 0x02000277 RID: 631
	[BurstCompile]
	internal static class MeshUtility
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		public static void GetMeshData(Mesh.MeshDataArray meshData, int meshIndex, out NativeArray<Vector3> vertices, out NativeArray<int> indices)
		{
			Mesh.MeshData meshData2 = meshData[meshIndex];
			vertices = new NativeArray<Vector3>(meshData2.vertexCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			meshData2.GetVertices(vertices);
			int num = 0;
			for (int i = 0; i < meshData2.subMeshCount; i++)
			{
				num += meshData2.GetSubMesh(i).indexCount;
			}
			indices = new NativeArray<int>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			int num2 = 0;
			for (int j = 0; j < meshData2.subMeshCount; j++)
			{
				SubMeshDescriptor subMesh = meshData2.GetSubMesh(j);
				meshData2.GetIndices(indices.GetSubArray(num2, subMesh.indexCount), j, true);
				num2 += subMesh.indexCount;
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005CCA2 File Offset: 0x0005AEA2
		[BurstCompile]
		public static void MakeTrianglesClockwise(ref UnsafeSpan<Int3> vertices, ref UnsafeSpan<int> triangles)
		{
			MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Invoke(ref vertices, ref triangles);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0005CCAC File Offset: 0x0005AEAC
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static void MakeTrianglesClockwise$BurstManaged(ref UnsafeSpan<Int3> vertices, ref UnsafeSpan<int> triangles)
		{
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (!VectorMath.IsClockwiseXZ(*vertices[*triangles[i]], *vertices[*triangles[i + 1]], *vertices[*triangles[i + 2]]))
				{
					int num = *triangles[i];
					*triangles[i] = *triangles[i + 2];
					*triangles[i + 2] = num;
				}
			}
		}

		// Token: 0x02000278 RID: 632
		[BurstCompile]
		public struct JobRemoveDuplicateVertices : IJob
		{
			// Token: 0x06000F12 RID: 3858 RVA: 0x0005CD34 File Offset: 0x0005AF34
			public static int3 cross(int3 x, int3 y)
			{
				return (x * y.yzx - x.yzx * y).yzx;
			}

			// Token: 0x06000F13 RID: 3859 RVA: 0x0005CD68 File Offset: 0x0005AF68
			public unsafe void Execute()
			{
				int num = 0;
				this.outputVertices->Reset();
				this.outputTriangles->Reset();
				this.outputTags->Reset();
				NativeParallelHashMap<Int3, int> nativeParallelHashMap = new NativeParallelHashMap<Int3, int>(this.vertices.Length, Allocator.Temp);
				NativeArray<int> nativeArray = new NativeArray<int>(this.vertices.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				int num2 = 0;
				for (int i = 0; i < this.vertices.Length; i++)
				{
					if (nativeParallelHashMap.TryAdd(this.vertices[i], num2))
					{
						nativeArray[i] = num2;
						this.outputVertices->Add<Int3>(this.vertices[i]);
						num2++;
					}
					else
					{
						nativeArray[i] = nativeParallelHashMap[this.vertices[i]];
					}
				}
				int j = 0;
				int num3 = 0;
				while (j < this.triangles.Length)
				{
					int num4 = this.triangles[j];
					int num5 = this.triangles[j + 1];
					int num6 = this.triangles[j + 2];
					if (math.all(MeshUtility.JobRemoveDuplicateVertices.cross(this.vertices.ReinterpretLoad<int3>(num5) - this.vertices.ReinterpretLoad<int3>(num4), this.vertices.ReinterpretLoad<int3>(num6) - this.vertices.ReinterpretLoad<int3>(num4)) == 0))
					{
						num++;
					}
					else
					{
						this.outputTriangles->Add<int3>(new int3(nativeArray[num4], nativeArray[num5], nativeArray[num6]));
						this.outputTags->Add<int>(this.tags[num3]);
					}
					j += 3;
					num3++;
				}
				if (num > 0)
				{
					Debug.LogWarning(string.Format("Input mesh contained {0} degenerate triangles. These have been removed.\nA degenerate triangle is a triangle with zero area. It resembles a line or a point.", num));
				}
			}

			// Token: 0x04000B42 RID: 2882
			[ReadOnly]
			public NativeArray<Int3> vertices;

			// Token: 0x04000B43 RID: 2883
			[ReadOnly]
			public NativeArray<int> triangles;

			// Token: 0x04000B44 RID: 2884
			[ReadOnly]
			public NativeArray<int> tags;

			// Token: 0x04000B45 RID: 2885
			public unsafe UnsafeAppendBuffer* outputVertices;

			// Token: 0x04000B46 RID: 2886
			public unsafe UnsafeAppendBuffer* outputTriangles;

			// Token: 0x04000B47 RID: 2887
			public unsafe UnsafeAppendBuffer* outputTags;
		}

		// Token: 0x02000279 RID: 633
		// (Invoke) Token: 0x06000F15 RID: 3861
		public delegate void MakeTrianglesClockwise_00000E12$PostfixBurstDelegate(ref UnsafeSpan<Int3> vertices, ref UnsafeSpan<int> triangles);

		// Token: 0x0200027A RID: 634
		internal static class MakeTrianglesClockwise_00000E12$BurstDirectCall
		{
			// Token: 0x06000F18 RID: 3864 RVA: 0x0005CF48 File Offset: 0x0005B148
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Pointer == 0)
				{
					MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.DeferredCompilation, methodof(MeshUtility.MakeTrianglesClockwise$BurstManaged(ref UnsafeSpan<Int3>, ref UnsafeSpan<int>)).MethodHandle, typeof(MeshUtility.MakeTrianglesClockwise_00000E12$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Pointer;
			}

			// Token: 0x06000F19 RID: 3865 RVA: 0x0005CF74 File Offset: 0x0005B174
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000F1A RID: 3866 RVA: 0x0005CF8C File Offset: 0x0005B18C
			public static void Constructor()
			{
				MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(MeshUtility.MakeTrianglesClockwise(ref UnsafeSpan<Int3>, ref UnsafeSpan<int>)).MethodHandle);
			}

			// Token: 0x06000F1B RID: 3867 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000F1C RID: 3868 RVA: 0x0005CF9D File Offset: 0x0005B19D
			// Note: this type is marked as 'beforefieldinit'.
			static MakeTrianglesClockwise_00000E12$BurstDirectCall()
			{
				MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.Constructor();
			}

			// Token: 0x06000F1D RID: 3869 RVA: 0x0005CFA4 File Offset: 0x0005B1A4
			public static void Invoke(ref UnsafeSpan<Int3> vertices, ref UnsafeSpan<int> triangles)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = MeshUtility.MakeTrianglesClockwise_00000E12$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						calli(System.Void(Pathfinding.Util.UnsafeSpan`1<Pathfinding.Int3>&,Pathfinding.Util.UnsafeSpan`1<System.Int32>&), ref vertices, ref triangles, functionPointer);
						return;
					}
				}
				MeshUtility.MakeTrianglesClockwise$BurstManaged(ref vertices, ref triangles);
			}

			// Token: 0x04000B48 RID: 2888
			private static IntPtr Pointer;

			// Token: 0x04000B49 RID: 2889
			private static IntPtr DeferredCompilation;
		}
	}
}
