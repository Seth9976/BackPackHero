using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D.Common;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000026 RID: 38
	[Preserve]
	[ExecuteInEditMode]
	[DefaultExecutionOrder(-1)]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("2D Animation/Sprite Skin")]
	[MovedFrom("UnityEngine.U2D.Experimental.Animation")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@7.0/manual/SpriteSkin.html")]
	public sealed class SpriteSkin : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003E4B File Offset: 0x0000204B
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003E53 File Offset: 0x00002053
		internal bool batchSkinning
		{
			get
			{
				return this.m_BatchSkinning;
			}
			set
			{
				this.m_BatchSkinning = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003E5C File Offset: 0x0000205C
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003E64 File Offset: 0x00002064
		internal bool autoRebind
		{
			get
			{
				return this.m_AutoRebind;
			}
			set
			{
				this.m_AutoRebind = value;
				this.CacheCurrentSprite(this.m_AutoRebind);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003E79 File Offset: 0x00002079
		private int GetSpriteInstanceID()
		{
			if (!(this.sprite != null))
			{
				return 0;
			}
			return this.sprite.GetInstanceID();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E96 File Offset: 0x00002096
		internal void Awake()
		{
			this.m_SpriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003EA4 File Offset: 0x000020A4
		private void OnEnable()
		{
			this.Awake();
			this.m_TransformsHash = 0;
			this.CacheCurrentSprite(false);
			this.OnEnableBatch();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003EC0 File Offset: 0x000020C0
		internal void OnEditorEnable()
		{
			this.Awake();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003EC8 File Offset: 0x000020C8
		private void CacheValidFlag()
		{
			this.m_IsValid = this.isValid;
			if (!this.m_IsValid)
			{
				this.DeactivateSkinning();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EE4 File Offset: 0x000020E4
		private void Reset()
		{
			this.Awake();
			if (base.isActiveAndEnabled)
			{
				this.CacheValidFlag();
				this.OnResetBatch();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F00 File Offset: 0x00002100
		internal void UseBatching(bool value)
		{
			if (this.m_UseBatching != value)
			{
				this.m_UseBatching = value;
				this.UseBatchingBatch();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F18 File Offset: 0x00002118
		internal NativeByteArray GetDeformedVertices(int spriteVertexCount)
		{
			if (this.sprite != null)
			{
				if (this.m_CurrentDeformVerticesLength != spriteVertexCount)
				{
					this.m_TransformsHash = 0;
					this.m_CurrentDeformVerticesLength = spriteVertexCount;
				}
			}
			else
			{
				this.m_CurrentDeformVerticesLength = 0;
			}
			this.m_DeformedVertices = BufferManager.instance.GetBuffer(base.GetInstanceID(), this.m_CurrentDeformVerticesLength);
			return this.m_DeformedVertices;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F75 File Offset: 0x00002175
		public bool HasCurrentDeformedVertices()
		{
			return this.m_IsValid && this.m_DataIndex >= 0 && SpriteSkinComposite.instance.HasDeformableBufferForSprite(this.m_DataIndex);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F9C File Offset: 0x0000219C
		internal NativeArray<byte> GetCurrentDeformedVertices()
		{
			if (!this.m_IsValid)
			{
				throw new InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (this.m_DataIndex < 0)
			{
				throw new InvalidOperationException("There are no currently deformed vertices.");
			}
			return SpriteSkinComposite.instance.GetDeformableBufferForSprite(this.m_DataIndex);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FD8 File Offset: 0x000021D8
		internal NativeSlice<PositionVertex> GetCurrentDeformedVertexPositions()
		{
			if (this.sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				throw new InvalidOperationException("This SpriteSkin has deformed tangents");
			}
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Position))
			{
				throw new InvalidOperationException("This SpriteSkin does not have deformed positions.");
			}
			return this.GetCurrentDeformedVertices().Slice<byte>().SliceConvert<PositionVertex>();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000402C File Offset: 0x0000222C
		internal NativeSlice<PositionTangentVertex> GetCurrentDeformedVertexPositionsAndTangents()
		{
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				throw new InvalidOperationException("This SpriteSkin does not have deformed tangents");
			}
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Position))
			{
				throw new InvalidOperationException("This SpriteSkin does not have deformed positions.");
			}
			return this.GetCurrentDeformedVertices().Slice<byte>().SliceConvert<PositionTangentVertex>();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004080 File Offset: 0x00002280
		public IEnumerable<Vector3> GetDeformedVertexPositionData()
		{
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Position))
			{
				throw new InvalidOperationException("Sprite does not have vertex position data.");
			}
			return new NativeCustomSliceEnumerator<Vector3>(this.GetCurrentDeformedVertices().Slice(this.sprite.GetVertexStreamOffset(VertexAttribute.Position)), this.sprite.GetVertexCount(), this.sprite.GetVertexStreamSize());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000040E0 File Offset: 0x000022E0
		public IEnumerable<Vector4> GetDeformedVertexTangentData()
		{
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				throw new InvalidOperationException("Sprite does not have vertex tangent data.");
			}
			return new NativeCustomSliceEnumerator<Vector4>(this.GetCurrentDeformedVertices().Slice(this.sprite.GetVertexStreamOffset(VertexAttribute.Tangent)), this.sprite.GetVertexCount(), this.sprite.GetVertexStreamSize());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000413D File Offset: 0x0000233D
		private void OnDisable()
		{
			this.DeactivateSkinning();
			BufferManager.instance.ReturnBuffer(base.GetInstanceID());
			this.OnDisableBatch();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000415C File Offset: 0x0000235C
		internal void OnLateUpdate()
		{
			this.CacheCurrentSprite(this.m_AutoRebind);
			if (this.isValid && !this.batchSkinning && base.enabled && (this.alwaysUpdate || this.spriteRenderer.isVisible))
			{
				int num = this.CalculateTransformHash();
				int num2 = this.sprite.GetVertexStreamSize() * this.sprite.GetVertexCount();
				if (num2 > 0 && this.m_TransformsHash != num)
				{
					NativeByteArray deformedVertices = this.GetDeformedVertices(num2);
					SpriteSkinUtility.Deform(this.sprite, base.gameObject.transform.worldToLocalMatrix, this.boneTransforms, deformedVertices.array);
					this.UpdateBounds(deformedVertices.array);
					InternalEngineBridge.SetDeformableBuffer(this.spriteRenderer, deformedVertices.array);
					this.m_TransformsHash = num;
					this.m_CurrentDeformSprite = this.GetSpriteInstanceID();
					return;
				}
			}
			else if (!InternalEngineBridge.IsUsingDeformableBuffer(this.spriteRenderer, IntPtr.Zero))
			{
				this.DeactivateSkinning();
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004258 File Offset: 0x00002458
		private void CacheCurrentSprite(bool rebind)
		{
			if (this.m_CurrentDeformSprite != this.GetSpriteInstanceID())
			{
				this.DeactivateSkinning();
				this.m_CurrentDeformSprite = this.GetSpriteInstanceID();
				if (rebind && this.m_CurrentDeformSprite > 0 && this.rootBone != null)
				{
					SpriteBone[] bones = this.sprite.GetBones();
					Transform[] array = new Transform[bones.Length];
					if (SpriteSkin.GetSpriteBonesTransforms(bones, this.rootBone, array))
					{
						this.boneTransforms = array;
					}
				}
				this.UpdateSpriteDeform();
				this.CacheValidFlag();
				this.m_TransformsHash = 0;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000042DB File Offset: 0x000024DB
		internal Sprite sprite
		{
			get
			{
				return this.spriteRenderer.sprite;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000042E8 File Offset: 0x000024E8
		internal SpriteRenderer spriteRenderer
		{
			get
			{
				return this.m_SpriteRenderer;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000042F0 File Offset: 0x000024F0
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000042F8 File Offset: 0x000024F8
		public Transform[] boneTransforms
		{
			get
			{
				return this.m_BoneTransforms;
			}
			internal set
			{
				this.m_BoneTransforms = value;
				this.CacheValidFlag();
				this.OnBoneTransformChanged();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000430D File Offset: 0x0000250D
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004315 File Offset: 0x00002515
		public Transform rootBone
		{
			get
			{
				return this.m_RootBone;
			}
			internal set
			{
				this.m_RootBone = value;
				this.CacheValidFlag();
				this.OnRootBoneTransformChanged();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000432A File Offset: 0x0000252A
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004332 File Offset: 0x00002532
		internal Bounds bounds
		{
			get
			{
				return this.m_Bounds;
			}
			set
			{
				this.m_Bounds = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000433B File Offset: 0x0000253B
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004343 File Offset: 0x00002543
		public bool alwaysUpdate
		{
			get
			{
				return this.m_AlwaysUpdate;
			}
			set
			{
				this.m_AlwaysUpdate = value;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000434C File Offset: 0x0000254C
		internal static bool GetSpriteBonesTransforms(SpriteBone[] spriteBones, Transform rootBone, Transform[] outTransform)
		{
			if (rootBone == null)
			{
				throw new ArgumentException("rootBone parameter cannot be null");
			}
			if (spriteBones == null)
			{
				throw new ArgumentException("spritebone parameter cannot be null");
			}
			if (outTransform == null)
			{
				throw new ArgumentException("outTransform parameter cannot be null");
			}
			if (spriteBones.Length != outTransform.Length)
			{
				throw new ArgumentException("spritebone and outTransform array length must be the same");
			}
			Bone[] componentsInChildren = rootBone.GetComponentsInChildren<Bone>();
			if (componentsInChildren != null && componentsInChildren.Length >= spriteBones.Length)
			{
				int i;
				for (i = 0; i < spriteBones.Length; i++)
				{
					string boneHash = spriteBones[i].guid;
					Bone bone = Array.Find<Bone>(componentsInChildren, (Bone x) => x.guid == boneHash);
					if (bone == null)
					{
						break;
					}
					outTransform[i] = bone.transform;
				}
				if (i >= spriteBones.Length)
				{
					return true;
				}
			}
			return SpriteSkin.GetSpriteBonesTranformFromPath(spriteBones, rootBone, outTransform);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000440C File Offset: 0x0000260C
		private static bool GetSpriteBonesTranformFromPath(SpriteBone[] spriteBones, Transform rootBone, Transform[] outNewBoneTransform)
		{
			string[] array = new string[spriteBones.Length];
			for (int i = 0; i < spriteBones.Length; i++)
			{
				if (array[i] == null)
				{
					SpriteSkin.CalculateBoneTransformsPath(i, spriteBones, array);
				}
				if (rootBone.name == spriteBones[i].name)
				{
					outNewBoneTransform[i] = rootBone;
				}
				else
				{
					Transform transform = rootBone.Find(array[i]);
					if (transform == null)
					{
						return false;
					}
					outNewBoneTransform[i] = transform;
				}
			}
			return true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004478 File Offset: 0x00002678
		private static void CalculateBoneTransformsPath(int index, SpriteBone[] spriteBones, string[] paths)
		{
			SpriteBone spriteBone = spriteBones[index];
			int parentId = spriteBone.parentId;
			string name = spriteBone.name;
			if (parentId != -1 && spriteBones[parentId].parentId != -1)
			{
				if (paths[parentId] == null)
				{
					SpriteSkin.CalculateBoneTransformsPath(spriteBone.parentId, spriteBones, paths);
				}
				paths[index] = string.Format("{0}/{1}", paths[parentId], name);
				return;
			}
			paths[index] = name;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000044D9 File Offset: 0x000026D9
		internal bool isValid
		{
			get
			{
				return this.Validate() == SpriteSkinValidationResult.Ready;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000044E8 File Offset: 0x000026E8
		internal void DeactivateSkinning()
		{
			Sprite sprite = this.spriteRenderer.sprite;
			if (sprite != null)
			{
				InternalEngineBridge.SetLocalAABB(this.spriteRenderer, sprite.bounds);
			}
			this.spriteRenderer.DeactivateDeformableBuffer();
			this.m_TransformsHash = 0;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000452D File Offset: 0x0000272D
		internal void ResetSprite()
		{
			this.m_CurrentDeformSprite = 0;
			this.CacheValidFlag();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000453C File Offset: 0x0000273C
		public void OnBeforeSerialize()
		{
			this.OnBeforeSerializeBatch();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004544 File Offset: 0x00002744
		public void OnAfterDeserialize()
		{
			this.OnAfterSerializeBatch();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000454C File Offset: 0x0000274C
		private void OnEnableBatch()
		{
			this.m_TransformId = base.gameObject.transform.GetInstanceID();
			this.UpdateSpriteDeform();
			if (this.m_UseBatching && !this.m_BatchSkinning)
			{
				this.CacheBoneTransformIds(true);
				SpriteSkinComposite.instance.AddSpriteSkin(this);
				this.m_BatchSkinning = true;
				return;
			}
			SpriteSkinComposite.instance.AddSpriteSkinForLateUpdate(this);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000045AA File Offset: 0x000027AA
		private void OnResetBatch()
		{
			if (this.m_UseBatching)
			{
				this.CacheBoneTransformIds(true);
				SpriteSkinComposite.instance.CopyToSpriteSkinData(this);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000045C6 File Offset: 0x000027C6
		private void OnDisableBatch()
		{
			this.RemoveTransformFromSpriteSkinComposite();
			SpriteSkinComposite.instance.RemoveSpriteSkin(this);
			SpriteSkinComposite.instance.RemoveSpriteSkinForLateUpdate(this);
			this.m_BatchSkinning = false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000045EC File Offset: 0x000027EC
		internal void UpdateSpriteDeform()
		{
			if (this.sprite == null)
			{
				this.m_SpriteUVs = NativeCustomSlice<Vector2>.Default();
				this.m_SpriteVertices = NativeCustomSlice<Vector3>.Default();
				this.m_SpriteTangents = NativeCustomSlice<Vector4>.Default();
				this.m_SpriteBoneWeights = NativeCustomSlice<BoneWeight>.Default();
				this.m_SpriteBindPoses = NativeCustomSlice<Matrix4x4>.Default();
				this.m_SpriteHasTangents = false;
				this.m_SpriteVertexStreamSize = 0;
				this.m_SpriteVertexCount = 0;
				this.m_SpriteTangentVertexOffset = 0;
			}
			else
			{
				this.m_SpriteUVs = new NativeCustomSlice<Vector2>(this.sprite.GetVertexAttribute(VertexAttribute.TexCoord0));
				this.m_SpriteVertices = new NativeCustomSlice<Vector3>(this.sprite.GetVertexAttribute(VertexAttribute.Position));
				this.m_SpriteTangents = new NativeCustomSlice<Vector4>(this.sprite.GetVertexAttribute(VertexAttribute.Tangent));
				this.m_SpriteBoneWeights = new NativeCustomSlice<BoneWeight>(this.sprite.GetVertexAttribute(VertexAttribute.BlendWeight));
				this.m_SpriteBindPoses = new NativeCustomSlice<Matrix4x4>(this.sprite.GetBindPoses());
				this.m_SpriteHasTangents = this.sprite.HasVertexAttribute(VertexAttribute.Tangent);
				this.m_SpriteVertexStreamSize = this.sprite.GetVertexStreamSize();
				this.m_SpriteVertexCount = this.sprite.GetVertexCount();
				this.m_SpriteTangentVertexOffset = this.sprite.GetVertexStreamOffset(VertexAttribute.Tangent);
			}
			SpriteSkinComposite.instance.CopyToSpriteSkinData(this);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004728 File Offset: 0x00002928
		private void CacheBoneTransformIds(bool forceUpdate = false)
		{
			if (!this.m_BoneCacheUpdateToDate || forceUpdate)
			{
				SpriteSkinComposite.instance.RemoveTransformById(this.m_RootBoneTransformId);
				if (this.rootBone != null)
				{
					this.m_RootBoneTransformId = this.rootBone.GetInstanceID();
					if (base.enabled)
					{
						SpriteSkinComposite.instance.AddSpriteSkinRootBoneTransform(this);
					}
				}
				else
				{
					this.m_RootBoneTransformId = 0;
				}
				if (this.boneTransforms != null)
				{
					int num = 0;
					for (int i = 0; i < this.boneTransforms.Length; i++)
					{
						if (this.boneTransforms[i] != null)
						{
							num++;
						}
					}
					if (this.m_BoneTransformId.IsCreated)
					{
						for (int j = 0; j < this.m_BoneTransformId.Length; j++)
						{
							SpriteSkinComposite.instance.RemoveTransformById(this.m_BoneTransformId[j]);
						}
						NativeArrayHelpers.ResizeIfNeeded<int>(ref this.m_BoneTransformId, num, Allocator.Persistent);
					}
					else
					{
						this.m_BoneTransformId = new NativeArray<int>(num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
					}
					this.m_BoneTransformIdNativeSlice = new NativeCustomSlice<int>(this.m_BoneTransformId);
					int k = 0;
					int num2 = 0;
					while (k < this.boneTransforms.Length)
					{
						if (this.boneTransforms[k] != null)
						{
							this.m_BoneTransformId[num2] = this.boneTransforms[k].GetInstanceID();
							num2++;
						}
						k++;
					}
					if (base.enabled)
					{
						SpriteSkinComposite.instance.AddSpriteSkinBoneTransform(this);
					}
				}
				else if (this.m_BoneTransformId.IsCreated)
				{
					NativeArrayHelpers.ResizeIfNeeded<int>(ref this.m_BoneTransformId, 0, Allocator.Persistent);
				}
				else
				{
					this.m_BoneTransformId = new NativeArray<int>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				}
				this.CacheValidFlag();
				this.m_BoneCacheUpdateToDate = true;
				SpriteSkinComposite.instance.CopyToSpriteSkinData(this);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000048D0 File Offset: 0x00002AD0
		private void UseBatchingBatch()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.m_UseBatching)
			{
				this.m_BatchSkinning = true;
				this.CacheBoneTransformIds(false);
				SpriteSkinComposite.instance.AddSpriteSkin(this);
				SpriteSkinComposite.instance.RemoveSpriteSkinForLateUpdate(this);
				return;
			}
			SpriteSkinComposite.instance.RemoveSpriteSkin(this);
			SpriteSkinComposite.instance.AddSpriteSkinForLateUpdate(this);
			this.RemoveTransformFromSpriteSkinComposite();
			this.m_BatchSkinning = false;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004938 File Offset: 0x00002B38
		private void RemoveTransformFromSpriteSkinComposite()
		{
			if (this.m_BoneTransformId.IsCreated)
			{
				for (int i = 0; i < this.m_BoneTransformId.Length; i++)
				{
					SpriteSkinComposite.instance.RemoveTransformById(this.m_BoneTransformId[i]);
				}
				this.m_BoneTransformId.Dispose();
			}
			SpriteSkinComposite.instance.RemoveTransformById(this.m_RootBoneTransformId);
			this.m_RootBoneTransformId = -1;
			this.m_BoneCacheUpdateToDate = false;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000049A8 File Offset: 0x00002BA8
		internal void CopyToSpriteSkinData(ref SpriteSkinData data, int spriteSkinIndex)
		{
			this.CacheBoneTransformIds(false);
			this.CacheCurrentSprite(this.m_AutoRebind);
			data.vertices = this.m_SpriteVertices;
			data.boneWeights = this.m_SpriteBoneWeights;
			data.bindPoses = this.m_SpriteBindPoses;
			data.tangents = this.m_SpriteTangents;
			data.hasTangents = this.m_SpriteHasTangents;
			data.spriteVertexStreamSize = this.m_SpriteVertexStreamSize;
			data.spriteVertexCount = this.m_SpriteVertexCount;
			data.tangentVertexOffset = this.m_SpriteTangentVertexOffset;
			data.transformId = this.m_TransformId;
			data.boneTransformId = this.m_BoneTransformIdNativeSlice;
			this.m_DataIndex = spriteSkinIndex;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004A48 File Offset: 0x00002C48
		internal bool NeedUpdateCompositeCache()
		{
			IntPtr intPtr = new IntPtr(this.sprite.GetVertexAttribute(VertexAttribute.TexCoord0).GetUnsafeReadOnlyPtr<Vector2>());
			bool flag = this.m_SpriteUVs.data != intPtr;
			if (flag)
			{
				this.UpdateSpriteDeform();
			}
			return flag;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004A88 File Offset: 0x00002C88
		internal bool BatchValidate()
		{
			this.CacheBoneTransformIds(false);
			this.CacheCurrentSprite(this.m_AutoRebind);
			bool flag = this.m_CurrentDeformSprite != 0;
			return this.m_IsValid && flag && this.spriteRenderer.enabled && (this.alwaysUpdate || this.spriteRenderer.isVisible);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private void OnBoneTransformChanged()
		{
			if (base.enabled)
			{
				this.CacheBoneTransformIds(true);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private void OnRootBoneTransformChanged()
		{
			if (base.enabled)
			{
				this.CacheBoneTransformIds(true);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnBeforeSerializeBatch()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnAfterSerializeBatch()
		{
		}

		// Token: 0x04000050 RID: 80
		[SerializeField]
		private Transform m_RootBone;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private Transform[] m_BoneTransforms = new Transform[0];

		// Token: 0x04000052 RID: 82
		[SerializeField]
		private Bounds m_Bounds;

		// Token: 0x04000053 RID: 83
		[SerializeField]
		private bool m_UseBatching = true;

		// Token: 0x04000054 RID: 84
		[SerializeField]
		private bool m_AlwaysUpdate = true;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		private bool m_AutoRebind;

		// Token: 0x04000056 RID: 86
		private NativeByteArray m_DeformedVertices;

		// Token: 0x04000057 RID: 87
		private int m_CurrentDeformVerticesLength;

		// Token: 0x04000058 RID: 88
		private SpriteRenderer m_SpriteRenderer;

		// Token: 0x04000059 RID: 89
		private int m_CurrentDeformSprite;

		// Token: 0x0400005A RID: 90
		private bool m_ForceSkinning;

		// Token: 0x0400005B RID: 91
		private bool m_BatchSkinning;

		// Token: 0x0400005C RID: 92
		private bool m_IsValid;

		// Token: 0x0400005D RID: 93
		private int m_TransformsHash;

		// Token: 0x0400005E RID: 94
		private int m_TransformId;

		// Token: 0x0400005F RID: 95
		private NativeArray<int> m_BoneTransformId;

		// Token: 0x04000060 RID: 96
		private int m_RootBoneTransformId;

		// Token: 0x04000061 RID: 97
		private NativeCustomSlice<Vector2> m_SpriteUVs;

		// Token: 0x04000062 RID: 98
		private NativeCustomSlice<Vector3> m_SpriteVertices;

		// Token: 0x04000063 RID: 99
		private NativeCustomSlice<Vector4> m_SpriteTangents;

		// Token: 0x04000064 RID: 100
		private NativeCustomSlice<BoneWeight> m_SpriteBoneWeights;

		// Token: 0x04000065 RID: 101
		private NativeCustomSlice<Matrix4x4> m_SpriteBindPoses;

		// Token: 0x04000066 RID: 102
		private NativeCustomSlice<int> m_BoneTransformIdNativeSlice;

		// Token: 0x04000067 RID: 103
		private bool m_SpriteHasTangents;

		// Token: 0x04000068 RID: 104
		private int m_SpriteVertexStreamSize;

		// Token: 0x04000069 RID: 105
		private int m_SpriteVertexCount;

		// Token: 0x0400006A RID: 106
		private int m_SpriteTangentVertexOffset;

		// Token: 0x0400006B RID: 107
		private int m_DataIndex = -1;

		// Token: 0x0400006C RID: 108
		private bool m_BoneCacheUpdateToDate;
	}
}
