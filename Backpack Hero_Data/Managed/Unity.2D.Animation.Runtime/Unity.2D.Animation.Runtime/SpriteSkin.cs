using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D.Common;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000026 RID: 38
	[AddComponentMenu("2D Animation/Sprite Skin")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@7.0/manual/SpriteSkin.html")]
	[MovedFrom("UnityEngine.U2D.Experimental.Animation")]
	[DisallowMultipleComponent]
	[DefaultExecutionOrder(-1)]
	[ExecuteInEditMode]
	[Preserve]
	[RequireComponent(typeof(SpriteRenderer))]
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
			return this.m_IsValid && this.m_CurrentDeformVerticesLength > 0 && this.m_DeformedVertices.IsCreated;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F98 File Offset: 0x00002198
		internal NativeArray<byte> GetCurrentDeformedVertices()
		{
			if (!this.m_IsValid)
			{
				throw new InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (this.m_CurrentDeformVerticesLength <= 0)
			{
				throw new InvalidOperationException("There are no currently deformed vertices.");
			}
			if (!this.m_DeformedVertices.IsCreated)
			{
				throw new InvalidOperationException("There are no currently deformed vertices.");
			}
			return this.m_DeformedVertices.array;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FF0 File Offset: 0x000021F0
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00004044 File Offset: 0x00002244
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

		// Token: 0x060000CA RID: 202 RVA: 0x00004098 File Offset: 0x00002298
		public IEnumerable<Vector3> GetDeformedVertexPositionData()
		{
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Position))
			{
				throw new InvalidOperationException("Sprite does not have vertex position data.");
			}
			return new NativeCustomSliceEnumerator<Vector3>(this.GetCurrentDeformedVertices().Slice(this.sprite.GetVertexStreamOffset(VertexAttribute.Position)), this.sprite.GetVertexCount(), this.sprite.GetVertexStreamSize());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000040F8 File Offset: 0x000022F8
		public IEnumerable<Vector4> GetDeformedVertexTangentData()
		{
			if (!this.sprite.HasVertexAttribute(VertexAttribute.Tangent))
			{
				throw new InvalidOperationException("Sprite does not have vertex tangent data.");
			}
			return new NativeCustomSliceEnumerator<Vector4>(this.GetCurrentDeformedVertices().Slice(this.sprite.GetVertexStreamOffset(VertexAttribute.Tangent)), this.sprite.GetVertexCount(), this.sprite.GetVertexStreamSize());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004155 File Offset: 0x00002355
		private void OnDisable()
		{
			this.DeactivateSkinning();
			BufferManager.instance.ReturnBuffer(base.GetInstanceID());
			this.OnDisableBatch();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004174 File Offset: 0x00002374
		private void LateUpdate()
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

		// Token: 0x060000CE RID: 206 RVA: 0x00004270 File Offset: 0x00002470
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
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000042F3 File Offset: 0x000024F3
		internal Sprite sprite
		{
			get
			{
				return this.spriteRenderer.sprite;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004300 File Offset: 0x00002500
		internal SpriteRenderer spriteRenderer
		{
			get
			{
				return this.m_SpriteRenderer;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004308 File Offset: 0x00002508
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004310 File Offset: 0x00002510
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
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004325 File Offset: 0x00002525
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000432D File Offset: 0x0000252D
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
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004342 File Offset: 0x00002542
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000434A File Offset: 0x0000254A
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
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004353 File Offset: 0x00002553
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000435B File Offset: 0x0000255B
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

		// Token: 0x060000D9 RID: 217 RVA: 0x00004364 File Offset: 0x00002564
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

		// Token: 0x060000DA RID: 218 RVA: 0x00004424 File Offset: 0x00002624
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

		// Token: 0x060000DB RID: 219 RVA: 0x00004490 File Offset: 0x00002690
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
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000044F1 File Offset: 0x000026F1
		internal bool isValid
		{
			get
			{
				return this.Validate() == SpriteSkinValidationResult.Ready;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004500 File Offset: 0x00002700
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

		// Token: 0x060000DE RID: 222 RVA: 0x00004545 File Offset: 0x00002745
		internal void ResetSprite()
		{
			this.m_CurrentDeformSprite = 0;
			this.CacheValidFlag();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004554 File Offset: 0x00002754
		public void OnBeforeSerialize()
		{
			this.OnBeforeSerializeBatch();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000455C File Offset: 0x0000275C
		public void OnAfterDeserialize()
		{
			this.OnAfterSerializeBatch();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnEnableBatch()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000026F3 File Offset: 0x000008F3
		internal void UpdateSpriteDeform()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnResetBatch()
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000026F3 File Offset: 0x000008F3
		private void UseBatchingBatch()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnDisableBatch()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnBoneTransformChanged()
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnRootBoneTransformChanged()
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000026F3 File Offset: 0x000008F3
		private void OnBeforeSerializeBatch()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000026F3 File Offset: 0x000008F3
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
	}
}
