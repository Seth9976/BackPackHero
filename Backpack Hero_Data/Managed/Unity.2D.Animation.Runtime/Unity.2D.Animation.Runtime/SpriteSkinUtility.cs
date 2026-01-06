using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.U2D.Common;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002B RID: 43
	internal static class SpriteSkinUtility
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004664 File Offset: 0x00002864
		internal static SpriteSkinValidationResult Validate(this SpriteSkin spriteSkin)
		{
			if (spriteSkin.spriteRenderer.sprite == null)
			{
				return SpriteSkinValidationResult.SpriteNotFound;
			}
			int length = spriteSkin.spriteRenderer.sprite.GetBindPoses().Length;
			if (length == 0)
			{
				return SpriteSkinValidationResult.SpriteHasNoSkinningInformation;
			}
			if (spriteSkin.rootBone == null)
			{
				return SpriteSkinValidationResult.RootTransformNotFound;
			}
			if (spriteSkin.boneTransforms == null)
			{
				return SpriteSkinValidationResult.InvalidTransformArray;
			}
			if (length != spriteSkin.boneTransforms.Length)
			{
				return SpriteSkinValidationResult.InvalidTransformArrayLength;
			}
			bool flag = false;
			foreach (Transform transform in spriteSkin.boneTransforms)
			{
				if (transform == null)
				{
					return SpriteSkinValidationResult.TransformArrayContainsNull;
				}
				if (transform == spriteSkin.rootBone)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return SpriteSkinValidationResult.RootNotFoundInTransformArray;
			}
			NativeSlice<BoneWeight> vertexAttribute = spriteSkin.sprite.GetVertexAttribute(VertexAttribute.BlendWeight);
			if (!BurstedSpriteSkinUtilities.ValidateBoneWeights(in vertexAttribute, length))
			{
				return SpriteSkinValidationResult.InvalidBoneWeights;
			}
			return SpriteSkinValidationResult.Ready;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004730 File Offset: 0x00002930
		internal static void CreateBoneHierarchy(this SpriteSkin spriteSkin)
		{
			if (spriteSkin.spriteRenderer.sprite == null)
			{
				throw new InvalidOperationException("SpriteRenderer has no Sprite set");
			}
			SpriteBone[] bones = spriteSkin.spriteRenderer.sprite.GetBones();
			Transform[] array = new Transform[bones.Length];
			Transform transform = null;
			for (int i = 0; i < bones.Length; i++)
			{
				SpriteSkinUtility.CreateGameObject(i, bones, array, spriteSkin.transform);
				if (bones[i].parentId < 0 && transform == null)
				{
					transform = array[i];
				}
			}
			spriteSkin.rootBone = transform;
			spriteSkin.boneTransforms = array;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000047C0 File Offset: 0x000029C0
		internal static int GetVertexStreamSize(this Sprite sprite)
		{
			int num = 12;
			if (sprite.HasVertexAttribute(VertexAttribute.Normal))
			{
				num += 12;
			}
			if (sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				num += 16;
			}
			return num;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000047F0 File Offset: 0x000029F0
		internal static int GetVertexStreamOffset(this Sprite sprite, VertexAttribute channel)
		{
			bool flag = sprite.HasVertexAttribute(VertexAttribute.Position);
			bool flag2 = sprite.HasVertexAttribute(VertexAttribute.Normal);
			bool flag3 = sprite.HasVertexAttribute(VertexAttribute.Tangent);
			switch (channel)
			{
			case VertexAttribute.Position:
				if (!flag)
				{
					return -1;
				}
				return 0;
			case VertexAttribute.Normal:
				if (!flag2)
				{
					return -1;
				}
				return 12;
			case VertexAttribute.Tangent:
				if (!flag3)
				{
					return -1;
				}
				if (!flag2)
				{
					return 12;
				}
				return 24;
			default:
				return -1;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004848 File Offset: 0x00002A48
		private static void CreateGameObject(int index, SpriteBone[] spriteBones, Transform[] transforms, Transform root)
		{
			if (transforms[index] == null)
			{
				SpriteBone spriteBone = spriteBones[index];
				if (spriteBone.parentId >= 0)
				{
					SpriteSkinUtility.CreateGameObject(spriteBone.parentId, spriteBones, transforms, root);
				}
				Transform transform = new GameObject(spriteBone.name).transform;
				if (spriteBone.parentId >= 0)
				{
					transform.SetParent(transforms[spriteBone.parentId]);
				}
				else
				{
					transform.SetParent(root);
				}
				transform.localPosition = spriteBone.position;
				transform.localRotation = spriteBone.rotation;
				transform.localScale = Vector3.one;
				transforms[index] = transform;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000048E0 File Offset: 0x00002AE0
		internal static void ResetBindPose(this SpriteSkin spriteSkin)
		{
			if (!spriteSkin.isValid)
			{
				throw new InvalidOperationException("SpriteSkin is not valid");
			}
			SpriteBone[] bones = spriteSkin.spriteRenderer.sprite.GetBones();
			Transform[] boneTransforms = spriteSkin.boneTransforms;
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				Transform transform = boneTransforms[i];
				SpriteBone spriteBone = bones[i];
				if (spriteBone.parentId != -1)
				{
					transform.localPosition = spriteBone.position;
					transform.localRotation = spriteBone.rotation;
					transform.localScale = Vector3.one;
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004964 File Offset: 0x00002B64
		private unsafe static int GetHash(Matrix4x4 matrix)
		{
			uint* ptr = (uint*)(&matrix);
			char* ptr2 = (char*)ptr;
			return (int)math.hash((void*)ptr2, 64, 0U);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004980 File Offset: 0x00002B80
		internal static int CalculateTransformHash(this SpriteSkin spriteSkin)
		{
			int num = 0;
			int num2 = SpriteSkinUtility.GetHash(spriteSkin.transform.localToWorldMatrix) >> num;
			num++;
			foreach (Transform transform in spriteSkin.boneTransforms)
			{
				num2 ^= SpriteSkinUtility.GetHash(transform.localToWorldMatrix) >> num;
				num = (num + 1) % 8;
			}
			return num2;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000049E0 File Offset: 0x00002BE0
		internal unsafe static void Deform(Sprite sprite, Matrix4x4 rootInv, NativeSlice<Vector3> vertices, NativeSlice<Vector4> tangents, NativeSlice<BoneWeight> boneWeights, NativeArray<Matrix4x4> boneTransforms, NativeSlice<Matrix4x4> bindPoses, NativeArray<byte> deformableVertices)
		{
			NativeSlice<float3> nativeSlice = vertices.SliceWithStride<float3>();
			NativeSlice<float4> nativeSlice2 = tangents.SliceWithStride<float4>();
			NativeSlice<float4x4> nativeSlice3 = bindPoses.SliceWithStride<float4x4>();
			int vertexCount = sprite.GetVertexCount();
			int vertexStreamSize = sprite.GetVertexStreamSize();
			NativeArray<float4x4> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float4x4>(boneTransforms.GetUnsafePtr<Matrix4x4>(), boneTransforms.Length, Allocator.None);
			byte* unsafePtr = (byte*)deformableVertices.GetUnsafePtr<byte>();
			NativeSlice<float3> nativeSlice4 = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float3>((void*)unsafePtr, vertexStreamSize, vertexCount);
			NativeSlice<float4> nativeSlice5 = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float4>((void*)unsafePtr, vertexStreamSize, 1);
			if (sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				nativeSlice5 = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float4>((void*)(unsafePtr + sprite.GetVertexStreamOffset(VertexAttribute.Tangent)), vertexStreamSize, vertexCount);
			}
			if (sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				SpriteSkinUtility.Deform(rootInv, nativeSlice, nativeSlice2, boneWeights, nativeArray, nativeSlice3, nativeSlice4, nativeSlice5);
				return;
			}
			SpriteSkinUtility.Deform(rootInv, nativeSlice, boneWeights, nativeArray, nativeSlice3, nativeSlice4);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004A9C File Offset: 0x00002C9C
		internal static void Deform(float4x4 rootInv, NativeSlice<float3> vertices, NativeSlice<BoneWeight> boneWeights, NativeArray<float4x4> boneTransforms, NativeSlice<float4x4> bindPoses, NativeSlice<float3> deformed)
		{
			if (boneTransforms.Length == 0)
			{
				return;
			}
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				float4x4 float4x = bindPoses[i];
				float4x4 float4x2 = boneTransforms[i];
				boneTransforms[i] = math.mul(rootInv, math.mul(float4x2, float4x));
			}
			for (int j = 0; j < vertices.Length; j++)
			{
				int boneIndex = boneWeights[j].boneIndex0;
				int boneIndex2 = boneWeights[j].boneIndex1;
				int boneIndex3 = boneWeights[j].boneIndex2;
				int boneIndex4 = boneWeights[j].boneIndex3;
				float3 @float = vertices[j];
				deformed[j] = math.transform(boneTransforms[boneIndex], @float) * boneWeights[j].weight0 + math.transform(boneTransforms[boneIndex2], @float) * boneWeights[j].weight1 + math.transform(boneTransforms[boneIndex3], @float) * boneWeights[j].weight2 + math.transform(boneTransforms[boneIndex4], @float) * boneWeights[j].weight3;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004C0C File Offset: 0x00002E0C
		internal static void Deform(float4x4 rootInv, NativeSlice<float3> vertices, NativeSlice<float4> tangents, NativeSlice<BoneWeight> boneWeights, NativeArray<float4x4> boneTransforms, NativeSlice<float4x4> bindPoses, NativeSlice<float3> deformed, NativeSlice<float4> deformedTangents)
		{
			if (boneTransforms.Length == 0)
			{
				return;
			}
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				float4x4 float4x = bindPoses[i];
				float4x4 float4x2 = boneTransforms[i];
				boneTransforms[i] = math.mul(rootInv, math.mul(float4x2, float4x));
			}
			for (int j = 0; j < vertices.Length; j++)
			{
				int boneIndex = boneWeights[j].boneIndex0;
				int boneIndex2 = boneWeights[j].boneIndex1;
				int boneIndex3 = boneWeights[j].boneIndex2;
				int boneIndex4 = boneWeights[j].boneIndex3;
				float3 @float = vertices[j];
				deformed[j] = math.transform(boneTransforms[boneIndex], @float) * boneWeights[j].weight0 + math.transform(boneTransforms[boneIndex2], @float) * boneWeights[j].weight1 + math.transform(boneTransforms[boneIndex3], @float) * boneWeights[j].weight2 + math.transform(boneTransforms[boneIndex4], @float) * boneWeights[j].weight3;
				float4 float2 = new float4(tangents[j].xyz, 0f);
				float2 = math.mul(boneTransforms[boneIndex], float2) * boneWeights[j].weight0 + math.mul(boneTransforms[boneIndex2], float2) * boneWeights[j].weight1 + math.mul(boneTransforms[boneIndex3], float2) * boneWeights[j].weight2 + math.mul(boneTransforms[boneIndex4], float2) * boneWeights[j].weight3;
				deformedTangents[j] = new float4(math.normalize(float2.xyz), tangents[j].w);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004E68 File Offset: 0x00003068
		internal static void Deform(Sprite sprite, Matrix4x4 invRoot, Transform[] boneTransformsArray, NativeArray<byte> deformVertexData)
		{
			NativeSlice<Vector3> vertexAttribute = sprite.GetVertexAttribute(VertexAttribute.Position);
			NativeSlice<Vector4> vertexAttribute2 = sprite.GetVertexAttribute(VertexAttribute.Tangent);
			NativeSlice<BoneWeight> vertexAttribute3 = sprite.GetVertexAttribute(VertexAttribute.BlendWeight);
			NativeArray<Matrix4x4> bindPoses = sprite.GetBindPoses();
			NativeArray<Matrix4x4> nativeArray = new NativeArray<Matrix4x4>(boneTransformsArray.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < boneTransformsArray.Length; i++)
			{
				nativeArray[i] = boneTransformsArray[i].localToWorldMatrix;
			}
			SpriteSkinUtility.Deform(sprite, invRoot, vertexAttribute, vertexAttribute2, vertexAttribute3, nativeArray, bindPoses, deformVertexData);
			nativeArray.Dispose();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004EE0 File Offset: 0x000030E0
		internal static void Bake(this SpriteSkin spriteSkin, NativeArray<byte> deformVertexData)
		{
			if (!spriteSkin.isValid)
			{
				throw new Exception("Bake error: invalid SpriteSkin");
			}
			Sprite sprite = spriteSkin.spriteRenderer.sprite;
			Transform[] boneTransforms = spriteSkin.boneTransforms;
			SpriteSkinUtility.Deform(sprite, Matrix4x4.identity, boneTransforms, deformVertexData);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004F20 File Offset: 0x00003120
		internal static void CalculateBounds(this SpriteSkin spriteSkin)
		{
			Sprite sprite = spriteSkin.sprite;
			NativeArray<byte> nativeArray = new NativeArray<byte>(sprite.GetVertexStreamSize() * sprite.GetVertexCount(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<Vector3>(nativeArray.GetUnsafePtr<byte>(), sprite.GetVertexStreamSize(), sprite.GetVertexCount());
			spriteSkin.Bake(nativeArray);
			spriteSkin.UpdateBounds(nativeArray);
			nativeArray.Dispose();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004F78 File Offset: 0x00003178
		internal static Bounds CalculateSpriteSkinBounds(NativeSlice<float3> deformablePositions)
		{
			float3 @float = deformablePositions[0];
			float3 float2 = deformablePositions[0];
			for (int i = 1; i < deformablePositions.Length; i++)
			{
				@float = math.min(@float, deformablePositions[i]);
				float2 = math.max(float2, deformablePositions[i]);
			}
			float3 float3 = (float2 - @float) * 0.5f;
			float3 float4 = @float + float3;
			return new Bounds
			{
				center = float4,
				extents = float3
			};
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000500C File Offset: 0x0000320C
		internal unsafe static void UpdateBounds(this SpriteSkin spriteSkin, NativeArray<byte> deformedVertices)
		{
			byte* unsafePtr = (byte*)deformedVertices.GetUnsafePtr<byte>();
			int vertexCount = spriteSkin.sprite.GetVertexCount();
			int vertexStreamSize = spriteSkin.sprite.GetVertexStreamSize();
			NativeSlice<float3> nativeSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<float3>((void*)unsafePtr, vertexStreamSize, vertexCount);
			spriteSkin.bounds = SpriteSkinUtility.CalculateSpriteSkinBounds(nativeSlice);
			InternalEngineBridge.SetLocalAABB(spriteSkin.spriteRenderer, spriteSkin.bounds);
		}
	}
}
