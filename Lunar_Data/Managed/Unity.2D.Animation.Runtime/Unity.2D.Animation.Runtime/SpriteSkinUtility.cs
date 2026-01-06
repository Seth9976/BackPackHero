using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.U2D.Common;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000035 RID: 53
	internal static class SpriteSkinUtility
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000620C File Offset: 0x0000440C
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

		// Token: 0x0600011F RID: 287 RVA: 0x000062D8 File Offset: 0x000044D8
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

		// Token: 0x06000120 RID: 288 RVA: 0x00006368 File Offset: 0x00004568
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

		// Token: 0x06000121 RID: 289 RVA: 0x00006398 File Offset: 0x00004598
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

		// Token: 0x06000122 RID: 290 RVA: 0x000063F0 File Offset: 0x000045F0
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

		// Token: 0x06000123 RID: 291 RVA: 0x00006488 File Offset: 0x00004688
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

		// Token: 0x06000124 RID: 292 RVA: 0x0000650C File Offset: 0x0000470C
		private unsafe static int GetHash(Matrix4x4 matrix)
		{
			uint* ptr = (uint*)(&matrix);
			char* ptr2 = (char*)ptr;
			return (int)math.hash((void*)ptr2, 64, 0U);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006528 File Offset: 0x00004728
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

		// Token: 0x06000126 RID: 294 RVA: 0x00006588 File Offset: 0x00004788
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

		// Token: 0x06000127 RID: 295 RVA: 0x00006644 File Offset: 0x00004844
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

		// Token: 0x06000128 RID: 296 RVA: 0x000067B4 File Offset: 0x000049B4
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

		// Token: 0x06000129 RID: 297 RVA: 0x00006A10 File Offset: 0x00004C10
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

		// Token: 0x0600012A RID: 298 RVA: 0x00006A88 File Offset: 0x00004C88
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

		// Token: 0x0600012B RID: 299 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal static void CalculateBounds(this SpriteSkin spriteSkin)
		{
			Sprite sprite = spriteSkin.sprite;
			NativeArray<byte> nativeArray = new NativeArray<byte>(sprite.GetVertexStreamSize() * sprite.GetVertexCount(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<Vector3>(nativeArray.GetUnsafePtr<byte>(), sprite.GetVertexStreamSize(), sprite.GetVertexCount());
			spriteSkin.Bake(nativeArray);
			spriteSkin.UpdateBounds(nativeArray);
			nativeArray.Dispose();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006B20 File Offset: 0x00004D20
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

		// Token: 0x0600012D RID: 301 RVA: 0x00006BB4 File Offset: 0x00004DB4
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
