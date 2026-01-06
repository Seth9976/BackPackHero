using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001AE RID: 430
	[BurstCompile]
	public static class ColliderMeshBuilder2D
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x000403F0 File Offset: 0x0003E5F0
		public unsafe static int GenerateMeshesFromColliders(Collider2D[] colliders, int numColliders, float maxError, out NativeArray<float3> outputVertices, out NativeArray<int> outputIndices, out NativeArray<ColliderMeshBuilder2D.ShapeMesh> outputShapeMeshes)
		{
			PhysicsShapeGroup2D physicsShapeGroup2D = new PhysicsShapeGroup2D(1, 8);
			NativeList<PhysicsShape2D> nativeList = new NativeList<PhysicsShape2D>(numColliders, Allocator.Temp);
			NativeList<Vector2> nativeList2 = new NativeList<Vector2>(numColliders * 4, Allocator.Temp);
			NativeList<Matrix4x4> nativeList3 = new NativeList<Matrix4x4>(numColliders, Allocator.Temp);
			NativeList<int> nativeList4 = new NativeList<int>(numColliders, Allocator.Temp);
			HashSet<Rigidbody2D> hashSet = new HashSet<Rigidbody2D>();
			int num = 0;
			for (int i = 0; i < numColliders; i++)
			{
				Collider2D collider2D = colliders[i];
				if (!(collider2D == null) && collider2D.shapeCount != 0)
				{
					Rigidbody2D attachedRigidbody = collider2D.attachedRigidbody;
					int num2;
					if (attachedRigidbody == null)
					{
						TilemapCollider2D tilemapCollider2D = collider2D as TilemapCollider2D;
						if (tilemapCollider2D != null)
						{
							tilemapCollider2D.ProcessTilemapChanges();
						}
						num2 = collider2D.GetShapes(physicsShapeGroup2D);
					}
					else
					{
						if (!hashSet.Add(attachedRigidbody))
						{
							goto IL_018C;
						}
						num2 = attachedRigidbody.GetShapes(physicsShapeGroup2D);
					}
					nativeList.Length += num2;
					nativeList2.Length += physicsShapeGroup2D.vertexCount;
					NativeArray<PhysicsShape2D> subArray = nativeList.AsArray().GetSubArray(nativeList.Length - num2, num2);
					NativeArray<Vector2> subArray2 = nativeList2.AsArray().GetSubArray(nativeList2.Length - physicsShapeGroup2D.vertexCount, physicsShapeGroup2D.vertexCount);
					physicsShapeGroup2D.GetShapeData(subArray, subArray2);
					for (int j = 0; j < num2; j++)
					{
						PhysicsShape2D physicsShape2D = subArray[j];
						physicsShape2D.vertexStartIndex += num;
						subArray[j] = physicsShape2D;
					}
					num += subArray2.Length;
					nativeList3.AddReplicate(physicsShapeGroup2D.localToWorldMatrix, num2);
					nativeList4.AddReplicate(i, num2);
				}
				IL_018C:;
			}
			NativeList<float3> nativeList5 = new NativeList<float3>(Allocator.Temp);
			NativeList<int3> nativeList6 = new NativeList<int3>(Allocator.Temp);
			UnsafeSpan<PhysicsShape2D> unsafeSpan = nativeList.AsUnsafeSpan<PhysicsShape2D>();
			UnsafeSpan<float2> unsafeSpan2 = nativeList2.AsUnsafeSpan<Vector2>().Reinterpret<float2>();
			UnsafeSpan<Matrix4x4> unsafeSpan3 = nativeList3.AsUnsafeSpan<Matrix4x4>();
			UnsafeSpan<int> unsafeSpan4 = nativeList4.AsUnsafeSpan<int>();
			outputShapeMeshes = new NativeArray<ColliderMeshBuilder2D.ShapeMesh>(nativeList.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh> unsafeSpan5 = outputShapeMeshes.AsUnsafeSpan<ColliderMeshBuilder2D.ShapeMesh>();
			int num3 = ColliderMeshBuilder2D.GenerateMeshesFromShapes(ref unsafeSpan, ref unsafeSpan2, ref unsafeSpan3, ref unsafeSpan4, UnsafeUtility.AsRef<UnsafeList<float3>>((void*)nativeList5.GetUnsafeList()), UnsafeUtility.AsRef<UnsafeList<int3>>((void*)nativeList6.GetUnsafeList()), ref unsafeSpan5, maxError);
			outputVertices = nativeList5.ToArray(Allocator.Persistent);
			outputIndices = new NativeArray<int>(nativeList6.AsArray().Reinterpret<int>(12), Allocator.Persistent);
			return num3;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00040658 File Offset: 0x0003E858
		private static void AddCapsuleMesh(float2 c1, float2 c2, ref Matrix4x4 shapeMatrix, float radius, float maxError, ref UnsafeList<float3> outputVertices, ref UnsafeList<int3> outputIndices, ref float3 mn, ref float3 mx)
		{
			int num = math.max(4, CircleGeometryUtilities.CircleSteps(shapeMatrix, radius, maxError));
			num = num / 2 + 1;
			radius *= CircleGeometryUtilities.CircleRadiusAdjustmentFactor(2 * (num - 1));
			Vector3 vector = new Vector3(c1.x, c1.y, 0f);
			Vector3 vector2 = new Vector3(c2.x, c2.y, 0f);
			float2 @float = math.normalizesafe(c2 - c1, default(float2));
			float2 float2 = new float2(-@float.y, @float.x);
			Vector3 vector3 = radius * new Vector3(float2.x, float2.y, 0f);
			Vector3 vector4 = radius * new Vector3(@float.x, @float.y, 0f);
			float num2 = 3.1415927f / (float)(num - 1);
			int length = outputVertices.Length;
			int num3 = length + num;
			outputVertices.Length += num * 2;
			for (int i = 0; i < num; i++)
			{
				float num4;
				float num5;
				math.sincos(num2 * (float)i, out num4, out num5);
				Vector3 vector5 = vector + num5 * vector3 - num4 * vector4;
				mn = math.min(mn, vector5);
				mx = math.max(mx, vector5);
				outputVertices[length + i] = vector5;
				vector5 = vector2 - num5 * vector3 + num4 * vector4;
				mn = math.min(mn, vector5);
				mx = math.max(mx, vector5);
				outputVertices[num3 + i] = vector5;
			}
			int length2 = outputIndices.Length;
			int num6 = length2 + num - 2;
			outputIndices.Length += (num - 2) * 2;
			for (int j = 1; j < num - 1; j++)
			{
				outputIndices[length2 + j - 1] = new int3(length, length + j, length + j + 1);
				outputIndices[num6 + j - 1] = new int3(num3, num3 + j, num3 + j + 1);
			}
			int3 @int = new int3(length, length + num - 1, num3);
			outputIndices.Add(in @int);
			@int = new int3(length, num3, num3 + num - 1);
			outputIndices.Add(in @int);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000408F0 File Offset: 0x0003EAF0
		[BurstCompile]
		public static int GenerateMeshesFromShapes(ref UnsafeSpan<PhysicsShape2D> shapes, ref UnsafeSpan<float2> vertices, ref UnsafeSpan<Matrix4x4> shapeMatrices, ref UnsafeSpan<int> groupIndices, ref UnsafeList<float3> outputVertices, ref UnsafeList<int3> outputIndices, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh> outputShapeMeshes, float maxError)
		{
			return ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Invoke(ref shapes, ref vertices, ref shapeMatrices, ref groupIndices, ref outputVertices, ref outputIndices, ref outputShapeMeshes, maxError);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00040904 File Offset: 0x0003EB04
		[BurstCompile]
		[MethodImpl(256)]
		public unsafe static int GenerateMeshesFromShapes$BurstManaged(ref UnsafeSpan<PhysicsShape2D> shapes, ref UnsafeSpan<float2> vertices, ref UnsafeSpan<Matrix4x4> shapeMatrices, ref UnsafeSpan<int> groupIndices, ref UnsafeList<float3> outputVertices, ref UnsafeList<int3> outputIndices, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh> outputShapeMeshes, float maxError)
		{
			int num = 0;
			float3 @float = new float3(float.MaxValue, float.MaxValue, float.MaxValue);
			float3 float2 = new float3(float.MinValue, float.MinValue, float.MinValue);
			int num2 = 0;
			for (int i = 0; i < shapes.Length; i++)
			{
				PhysicsShape2D physicsShape2D = *shapes[i];
				UnsafeSpan<float2> unsafeSpan = vertices.Slice(physicsShape2D.vertexStartIndex, physicsShape2D.vertexCount);
				Matrix4x4 matrix4x = *shapeMatrices[i];
				switch (physicsShape2D.shapeType)
				{
				case PhysicsShapeType2D.Circle:
				{
					int num3 = CircleGeometryUtilities.CircleSteps(matrix4x, physicsShape2D.radius, maxError);
					float num4 = physicsShape2D.radius * CircleGeometryUtilities.CircleRadiusAdjustmentFactor(num3);
					Vector3 vector = new Vector3(unsafeSpan[0].x, unsafeSpan[0].y, 0f);
					Vector3 vector2 = new Vector3(num4, 0f, 0f);
					Vector3 vector3 = new Vector3(0f, num4, 0f);
					float num5 = 6.2831855f / (float)num3;
					int length = outputVertices.Length;
					for (int j = 0; j < num3; j++)
					{
						float num6;
						float num7;
						math.sincos(num5 * (float)j, out num6, out num7);
						Vector3 vector4 = vector + num7 * vector2 + num6 * vector3;
						@float = math.min(@float, vector4);
						float2 = math.max(float2, vector4);
						float3 float3 = vector4;
						outputVertices.Add(in float3);
					}
					for (int k = 1; k < num3; k++)
					{
						int3 @int = new int3(length, length + k, length + (k + 1) % num3);
						outputIndices.Add(in @int);
					}
					break;
				}
				case PhysicsShapeType2D.Capsule:
				{
					float2 float4 = *unsafeSpan[0];
					float2 float5 = *unsafeSpan[1];
					ColliderMeshBuilder2D.AddCapsuleMesh(float4, float5, ref matrix4x, physicsShape2D.radius, maxError, ref outputVertices, ref outputIndices, ref @float, ref float2);
					break;
				}
				case PhysicsShapeType2D.Polygon:
				{
					int length2 = outputVertices.Length;
					outputVertices.Resize(length2 + physicsShape2D.vertexCount, NativeArrayOptions.UninitializedMemory);
					for (int l = 0; l < physicsShape2D.vertexCount; l++)
					{
						Vector3 vector5 = new Vector3(unsafeSpan[l].x, unsafeSpan[l].y, 0f);
						@float = math.min(@float, vector5);
						float2 = math.max(float2, vector5);
						outputVertices[length2 + l] = vector5;
					}
					outputIndices.SetCapacity(math.ceilpow2(outputIndices.Length + (physicsShape2D.vertexCount - 2)));
					for (int m = 1; m < physicsShape2D.vertexCount - 1; m++)
					{
						outputIndices.AddNoResize(new int3(length2, length2 + m, length2 + m + 1));
					}
					break;
				}
				case PhysicsShapeType2D.Edges:
					if (physicsShape2D.radius > maxError)
					{
						for (int n = 0; n < physicsShape2D.vertexCount - 1; n++)
						{
							ColliderMeshBuilder2D.AddCapsuleMesh(*unsafeSpan[n], *unsafeSpan[n + 1], ref matrix4x, physicsShape2D.radius, maxError, ref outputVertices, ref outputIndices, ref @float, ref float2);
						}
					}
					else
					{
						int length3 = outputVertices.Length;
						outputVertices.Resize(length3 + physicsShape2D.vertexCount, NativeArrayOptions.UninitializedMemory);
						for (int num8 = 0; num8 < physicsShape2D.vertexCount; num8++)
						{
							Vector3 vector6 = new Vector3(unsafeSpan[num8].x, unsafeSpan[num8].y, 0f);
							@float = math.min(@float, vector6);
							float2 = math.max(float2, vector6);
							outputVertices[length3 + num8] = vector6;
						}
						outputIndices.SetCapacity(math.ceilpow2(outputIndices.Length + (physicsShape2D.vertexCount - 1)));
						for (int num9 = 0; num9 < physicsShape2D.vertexCount - 1; num9++)
						{
							outputIndices.AddNoResize(new int3(length3 + num9, length3 + num9 + 1, length3 + num9 + 1));
						}
					}
					break;
				default:
					throw new Exception("Unexpected PhysicsShapeType2D");
				}
				if (i == shapes.Length - 1 || *groupIndices[i] != *groupIndices[i + 1] || outputIndices.Length - num > 100)
				{
					ToWorldMatrix toWorldMatrix = new ToWorldMatrix(new float3x3(matrix4x));
					Bounds bounds = new Bounds((@float + float2) * 0.5f, float2 - @float);
					bounds = toWorldMatrix.ToWorld(bounds);
					bounds.center += matrix4x.GetColumn(3);
					*outputShapeMeshes[num2++] = new ColliderMeshBuilder2D.ShapeMesh
					{
						bounds = bounds,
						matrix = matrix4x,
						startIndex = num * 3,
						endIndex = outputIndices.Length * 3,
						tag = *groupIndices[i]
					};
					@float = new float3(float.MaxValue, float.MaxValue, float.MaxValue);
					float2 = new float3(float.MinValue, float.MinValue, float.MinValue);
					num = outputIndices.Length;
				}
			}
			return num2;
		}

		// Token: 0x020001AF RID: 431
		public struct ShapeMesh
		{
			// Token: 0x040007D8 RID: 2008
			public Matrix4x4 matrix;

			// Token: 0x040007D9 RID: 2009
			public Bounds bounds;

			// Token: 0x040007DA RID: 2010
			public int startIndex;

			// Token: 0x040007DB RID: 2011
			public int endIndex;

			// Token: 0x040007DC RID: 2012
			public int tag;
		}

		// Token: 0x020001B0 RID: 432
		// (Invoke) Token: 0x06000B6F RID: 2927
		public delegate int GenerateMeshesFromShapes_00000A8F$PostfixBurstDelegate(ref UnsafeSpan<PhysicsShape2D> shapes, ref UnsafeSpan<float2> vertices, ref UnsafeSpan<Matrix4x4> shapeMatrices, ref UnsafeSpan<int> groupIndices, ref UnsafeList<float3> outputVertices, ref UnsafeList<int3> outputIndices, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh> outputShapeMeshes, float maxError);

		// Token: 0x020001B1 RID: 433
		internal static class GenerateMeshesFromShapes_00000A8F$BurstDirectCall
		{
			// Token: 0x06000B72 RID: 2930 RVA: 0x00040E5F File Offset: 0x0003F05F
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Pointer == 0)
				{
					ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.DeferredCompilation, methodof(ColliderMeshBuilder2D.GenerateMeshesFromShapes$BurstManaged(ref UnsafeSpan<PhysicsShape2D>, ref UnsafeSpan<float2>, ref UnsafeSpan<Matrix4x4>, ref UnsafeSpan<int>, ref UnsafeList<float3>, ref UnsafeList<int3>, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh>, float)).MethodHandle, typeof(ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Pointer;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x00040E8C File Offset: 0x0003F08C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x00040EA4 File Offset: 0x0003F0A4
			public static void Constructor()
			{
				ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(ColliderMeshBuilder2D.GenerateMeshesFromShapes(ref UnsafeSpan<PhysicsShape2D>, ref UnsafeSpan<float2>, ref UnsafeSpan<Matrix4x4>, ref UnsafeSpan<int>, ref UnsafeList<float3>, ref UnsafeList<int3>, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh>, float)).MethodHandle);
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x000033F6 File Offset: 0x000015F6
			public static void Initialize()
			{
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00040EB5 File Offset: 0x0003F0B5
			// Note: this type is marked as 'beforefieldinit'.
			static GenerateMeshesFromShapes_00000A8F$BurstDirectCall()
			{
				ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.Constructor();
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x00040EBC File Offset: 0x0003F0BC
			public static int Invoke(ref UnsafeSpan<PhysicsShape2D> shapes, ref UnsafeSpan<float2> vertices, ref UnsafeSpan<Matrix4x4> shapeMatrices, ref UnsafeSpan<int> groupIndices, ref UnsafeList<float3> outputVertices, ref UnsafeList<int3> outputIndices, ref UnsafeSpan<ColliderMeshBuilder2D.ShapeMesh> outputShapeMeshes, float maxError)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = ColliderMeshBuilder2D.GenerateMeshesFromShapes_00000A8F$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(Pathfinding.Util.UnsafeSpan`1<UnityEngine.PhysicsShape2D>&,Pathfinding.Util.UnsafeSpan`1<Unity.Mathematics.float2>&,Pathfinding.Util.UnsafeSpan`1<UnityEngine.Matrix4x4>&,Pathfinding.Util.UnsafeSpan`1<System.Int32>&,Unity.Collections.LowLevel.Unsafe.UnsafeList`1<Unity.Mathematics.float3>&,Unity.Collections.LowLevel.Unsafe.UnsafeList`1<Unity.Mathematics.int3>&,Pathfinding.Util.UnsafeSpan`1<Pathfinding.Graphs.Navmesh.ColliderMeshBuilder2D/ShapeMesh>&,System.Single), ref shapes, ref vertices, ref shapeMatrices, ref groupIndices, ref outputVertices, ref outputIndices, ref outputShapeMeshes, maxError, functionPointer);
					}
				}
				return ColliderMeshBuilder2D.GenerateMeshesFromShapes$BurstManaged(ref shapes, ref vertices, ref shapeMatrices, ref groupIndices, ref outputVertices, ref outputIndices, ref outputShapeMeshes, maxError);
			}

			// Token: 0x040007DD RID: 2013
			private static IntPtr Pointer;

			// Token: 0x040007DE RID: 2014
			private static IntPtr DeferredCompilation;
		}
	}
}
