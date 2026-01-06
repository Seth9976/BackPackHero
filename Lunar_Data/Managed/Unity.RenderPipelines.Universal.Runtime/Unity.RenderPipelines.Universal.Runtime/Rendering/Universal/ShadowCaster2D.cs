using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000034 RID: 52
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Rendering/2D/Shadow Caster 2D")]
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	public class ShadowCaster2D : ShadowCasterGroup2D, ISerializationCallbackReceiver
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		public Mesh mesh
		{
			get
			{
				return this.m_Mesh;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00010AB1 File Offset: 0x0000ECB1
		public Vector3[] shapePath
		{
			get
			{
				return this.m_ShapePath;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00010AB9 File Offset: 0x0000ECB9
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00010AC1 File Offset: 0x0000ECC1
		internal int shapePathHash
		{
			get
			{
				return this.m_ShapePathHash;
			}
			set
			{
				this.m_ShapePathHash = value;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010ACC File Offset: 0x0000ECCC
		internal override void CacheValues()
		{
			this.m_CachedPosition = base.transform.position;
			this.m_CachedLossyScale = base.transform.lossyScale;
			this.m_CachedRotation = base.transform.rotation;
			this.m_CachedShadowMatrix = Matrix4x4.TRS(this.m_CachedPosition, this.m_CachedRotation, Vector3.one);
			this.m_CachedInverseShadowMatrix = this.m_CachedShadowMatrix.inverse;
			this.m_CachedLocalToWorldMatrix = base.transform.localToWorldMatrix;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00010B53 File Offset: 0x0000ED53
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00010B4A File Offset: 0x0000ED4A
		public bool useRendererSilhouette
		{
			get
			{
				return this.m_UseRendererSilhouette && this.m_HasRenderer;
			}
			set
			{
				this.m_UseRendererSilhouette = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00010B6E File Offset: 0x0000ED6E
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00010B65 File Offset: 0x0000ED65
		public bool selfShadows
		{
			get
			{
				return this.m_SelfShadows;
			}
			set
			{
				this.m_SelfShadows = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00010B7F File Offset: 0x0000ED7F
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00010B76 File Offset: 0x0000ED76
		public bool castsShadows
		{
			get
			{
				return this.m_CastsShadows;
			}
			set
			{
				this.m_CastsShadows = value;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00010B88 File Offset: 0x0000ED88
		private static int[] SetDefaultSortingLayers()
		{
			int num = SortingLayer.layers.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = SortingLayer.layers[i].id;
			}
			return array;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		internal bool IsLit(Light2D light)
		{
			Vector3 vector;
			vector.x = light.m_CachedPosition.x - this.m_BoundingSphere.position.x;
			vector.y = light.m_CachedPosition.y - this.m_BoundingSphere.position.y;
			vector.z = light.m_CachedPosition.z - this.m_BoundingSphere.position.z;
			float num = Vector3.SqrMagnitude(vector);
			float num2 = light.boundingSphere.radius + this.m_BoundingSphere.radius;
			return num <= num2 * num2;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010C60 File Offset: 0x0000EE60
		internal bool IsShadowedLayer(int layer)
		{
			return this.m_ApplyToSortingLayers != null && Array.IndexOf<int>(this.m_ApplyToSortingLayers, layer) >= 0;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00010C80 File Offset: 0x0000EE80
		private void Awake()
		{
			if (this.m_ApplyToSortingLayers == null)
			{
				this.m_ApplyToSortingLayers = ShadowCaster2D.SetDefaultSortingLayers();
			}
			Bounds bounds = new Bounds(base.transform.position, Vector3.one);
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				bounds = component.bounds;
			}
			else
			{
				Collider2D component2 = base.GetComponent<Collider2D>();
				if (component2 != null)
				{
					bounds = component2.bounds;
				}
			}
			Vector3 zero = Vector3.zero;
			Vector3 position = base.transform.position;
			if (base.transform.lossyScale.x != 0f && base.transform.lossyScale.y != 0f)
			{
				zero = new Vector3(1f / base.transform.lossyScale.x, 1f / base.transform.lossyScale.y);
				position = new Vector3(zero.x * -base.transform.position.x, zero.y * -base.transform.position.y);
			}
			if (this.m_ShapePath == null || this.m_ShapePath.Length == 0)
			{
				this.m_ShapePath = new Vector3[]
				{
					position + new Vector3(zero.x * bounds.min.x, zero.y * bounds.min.y),
					position + new Vector3(zero.x * bounds.min.x, zero.y * bounds.max.y),
					position + new Vector3(zero.x * bounds.max.x, zero.y * bounds.max.y),
					position + new Vector3(zero.x * bounds.max.x, zero.y * bounds.min.y)
				};
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00010E98 File Offset: 0x0000F098
		protected void OnEnable()
		{
			if (this.m_Mesh == null || this.m_InstanceId != base.GetInstanceID())
			{
				this.m_Mesh = new Mesh();
				this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, this.m_ShapePath);
				this.m_InstanceId = base.GetInstanceID();
			}
			this.m_ShadowCasterGroup = null;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010EF6 File Offset: 0x0000F0F6
		protected void OnDisable()
		{
			ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, this.m_ShadowCasterGroup);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00010F04 File Offset: 0x0000F104
		public void Update()
		{
			Renderer renderer;
			this.m_HasRenderer = base.TryGetComponent<Renderer>(out renderer);
			if (LightUtility.CheckForChange(this.m_ShapePathHash, ref this.m_PreviousPathHash))
			{
				this.m_LocalBounds = ShadowUtility.GenerateShadowMesh(this.m_Mesh, this.m_ShapePath);
			}
			this.m_PreviousShadowCasterGroup = this.m_ShadowCasterGroup;
			if (ShadowCasterGroup2DManager.AddToShadowCasterGroup(this, ref this.m_ShadowCasterGroup) && this.m_ShadowCasterGroup != null)
			{
				if (this.m_PreviousShadowCasterGroup == this)
				{
					ShadowCasterGroup2DManager.RemoveGroup(this);
				}
				ShadowCasterGroup2DManager.RemoveFromShadowCasterGroup(this, this.m_PreviousShadowCasterGroup);
				if (this.m_ShadowCasterGroup == this)
				{
					ShadowCasterGroup2DManager.AddGroup(this);
				}
			}
			if (LightUtility.CheckForChange(this.m_ShadowGroup, ref this.m_PreviousShadowGroup))
			{
				ShadowCasterGroup2DManager.RemoveGroup(this);
				ShadowCasterGroup2DManager.AddGroup(this);
			}
			if (LightUtility.CheckForChange(this.m_CastsShadows, ref this.m_PreviousCastsShadows))
			{
				if (this.m_CastsShadows)
				{
					ShadowCasterGroup2DManager.AddGroup(this);
				}
				else
				{
					ShadowCasterGroup2DManager.RemoveGroup(this);
				}
			}
			this.UpdateBoundingSphere();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010FF3 File Offset: 0x0000F1F3
		public void OnBeforeSerialize()
		{
			this.m_ComponentVersion = ShadowCaster2D.ComponentVersions.Version_1;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public void OnAfterDeserialize()
		{
			if (this.m_ComponentVersion == ShadowCaster2D.ComponentVersions.Version_Unserialized)
			{
				this.m_LocalBounds = ShadowUtility.CalculateLocalBounds(this.m_ShapePath);
				this.m_ComponentVersion = ShadowCaster2D.ComponentVersions.Version_1;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00011020 File Offset: 0x0000F220
		private void UpdateBoundingSphere()
		{
			Vector3 vector = base.transform.TransformPoint(this.m_LocalBounds.max);
			Vector3 vector2 = base.transform.TransformPoint(this.m_LocalBounds.min);
			Vector3 vector3 = 0.5f * (vector + vector2);
			float num = Vector3.Magnitude(vector - vector3);
			this.m_BoundingSphere = new BoundingSphere(vector3, num);
		}

		// Token: 0x0400015A RID: 346
		private const ShadowCaster2D.ComponentVersions k_CurrentComponentVersion = ShadowCaster2D.ComponentVersions.Version_1;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		private ShadowCaster2D.ComponentVersions m_ComponentVersion;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private bool m_HasRenderer;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private bool m_UseRendererSilhouette = true;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		private bool m_CastsShadows = true;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private bool m_SelfShadows;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private int[] m_ApplyToSortingLayers;

		// Token: 0x04000161 RID: 353
		[SerializeField]
		private Vector3[] m_ShapePath;

		// Token: 0x04000162 RID: 354
		[SerializeField]
		private int m_ShapePathHash;

		// Token: 0x04000163 RID: 355
		[SerializeField]
		private Mesh m_Mesh;

		// Token: 0x04000164 RID: 356
		[SerializeField]
		private int m_InstanceId;

		// Token: 0x04000165 RID: 357
		internal ShadowCasterGroup2D m_ShadowCasterGroup;

		// Token: 0x04000166 RID: 358
		internal ShadowCasterGroup2D m_PreviousShadowCasterGroup;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		internal Bounds m_LocalBounds;

		// Token: 0x04000168 RID: 360
		internal BoundingSphere m_BoundingSphere;

		// Token: 0x04000169 RID: 361
		private int m_PreviousShadowGroup;

		// Token: 0x0400016A RID: 362
		private bool m_PreviousCastsShadows = true;

		// Token: 0x0400016B RID: 363
		private int m_PreviousPathHash;

		// Token: 0x0400016C RID: 364
		internal Vector3 m_CachedPosition;

		// Token: 0x0400016D RID: 365
		internal Vector3 m_CachedLossyScale;

		// Token: 0x0400016E RID: 366
		internal Quaternion m_CachedRotation;

		// Token: 0x0400016F RID: 367
		internal Matrix4x4 m_CachedShadowMatrix;

		// Token: 0x04000170 RID: 368
		internal Matrix4x4 m_CachedInverseShadowMatrix;

		// Token: 0x04000171 RID: 369
		internal Matrix4x4 m_CachedLocalToWorldMatrix;

		// Token: 0x0200014B RID: 331
		public enum ComponentVersions
		{
			// Token: 0x040008C3 RID: 2243
			Version_Unserialized,
			// Token: 0x040008C4 RID: 2244
			Version_1
		}
	}
}
