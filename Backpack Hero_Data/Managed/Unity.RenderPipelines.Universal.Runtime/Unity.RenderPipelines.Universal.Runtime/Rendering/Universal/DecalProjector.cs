using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000062 RID: 98
	[ExecuteAlways]
	[AddComponentMenu("Rendering/URP Decal Projector")]
	public class DecalProjector : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600036C RID: 876 RVA: 0x00014E9C File Offset: 0x0001309C
		// (remove) Token: 0x0600036D RID: 877 RVA: 0x00014ED0 File Offset: 0x000130D0
		internal static event DecalProjector.DecalProjectorAction onDecalAdd;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600036E RID: 878 RVA: 0x00014F04 File Offset: 0x00013104
		// (remove) Token: 0x0600036F RID: 879 RVA: 0x00014F38 File Offset: 0x00013138
		internal static event DecalProjector.DecalProjectorAction onDecalRemove;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000370 RID: 880 RVA: 0x00014F6C File Offset: 0x0001316C
		// (remove) Token: 0x06000371 RID: 881 RVA: 0x00014FA0 File Offset: 0x000131A0
		internal static event DecalProjector.DecalProjectorAction onDecalPropertyChange;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000372 RID: 882 RVA: 0x00014FD4 File Offset: 0x000131D4
		// (remove) Token: 0x06000373 RID: 883 RVA: 0x00015008 File Offset: 0x00013208
		internal static event DecalProjector.DecalProjectorAction onDecalMaterialChange;

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001503B File Offset: 0x0001323B
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00015042 File Offset: 0x00013242
		internal static Material defaultMaterial { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0001504A File Offset: 0x0001324A
		internal static bool isSupported
		{
			get
			{
				return DecalProjector.onDecalAdd != null;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015054 File Offset: 0x00013254
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0001505C File Offset: 0x0001325C
		internal DecalEntity decalEntity { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00015065 File Offset: 0x00013265
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0001506D File Offset: 0x0001326D
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
				this.OnValidate();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001507C File Offset: 0x0001327C
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00015084 File Offset: 0x00013284
		public float drawDistance
		{
			get
			{
				return this.m_DrawDistance;
			}
			set
			{
				this.m_DrawDistance = Mathf.Max(0f, value);
				this.OnValidate();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001509D File Offset: 0x0001329D
		// (set) Token: 0x0600037E RID: 894 RVA: 0x000150A5 File Offset: 0x000132A5
		public float fadeScale
		{
			get
			{
				return this.m_FadeScale;
			}
			set
			{
				this.m_FadeScale = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000150B9 File Offset: 0x000132B9
		// (set) Token: 0x06000380 RID: 896 RVA: 0x000150C1 File Offset: 0x000132C1
		public float startAngleFade
		{
			get
			{
				return this.m_StartAngleFade;
			}
			set
			{
				this.m_StartAngleFade = Mathf.Clamp(value, 0f, 180f);
				this.OnValidate();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000150DF File Offset: 0x000132DF
		// (set) Token: 0x06000382 RID: 898 RVA: 0x000150E7 File Offset: 0x000132E7
		public float endAngleFade
		{
			get
			{
				return this.m_EndAngleFade;
			}
			set
			{
				this.m_EndAngleFade = Mathf.Clamp(value, this.m_StartAngleFade, 180f);
				this.OnValidate();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00015106 File Offset: 0x00013306
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0001510E File Offset: 0x0001330E
		public Vector2 uvScale
		{
			get
			{
				return this.m_UVScale;
			}
			set
			{
				this.m_UVScale = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001511D File Offset: 0x0001331D
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00015125 File Offset: 0x00013325
		public Vector2 uvBias
		{
			get
			{
				return this.m_UVBias;
			}
			set
			{
				this.m_UVBias = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00015134 File Offset: 0x00013334
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0001513C File Offset: 0x0001333C
		public DecalScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				this.m_ScaleMode = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001514B File Offset: 0x0001334B
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00015153 File Offset: 0x00013353
		public Vector3 pivot
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00015162 File Offset: 0x00013362
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0001516A File Offset: 0x0001336A
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00015179 File Offset: 0x00013379
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00015181 File Offset: 0x00013381
		public float fadeFactor
		{
			get
			{
				return this.m_FadeFactor;
			}
			set
			{
				this.m_FadeFactor = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00015195 File Offset: 0x00013395
		internal Vector3 effectiveScale
		{
			get
			{
				if (this.m_ScaleMode != DecalScaleMode.InheritFromHierarchy)
				{
					return Vector3.one;
				}
				return base.transform.lossyScale;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000151B1 File Offset: 0x000133B1
		internal Vector3 decalSize
		{
			get
			{
				return new Vector3(this.m_Size.x, this.m_Size.z, this.m_Size.y);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000151D9 File Offset: 0x000133D9
		internal Vector3 decalOffset
		{
			get
			{
				return new Vector3(this.m_Offset.x, -this.m_Offset.z, this.m_Offset.y);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00015202 File Offset: 0x00013402
		internal Vector4 uvScaleBias
		{
			get
			{
				return new Vector4(this.m_UVScale.x, this.m_UVScale.y, this.m_UVBias.x, this.m_UVBias.y);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00015235 File Offset: 0x00013435
		private void InitMaterial()
		{
			this.m_Material == null;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00015244 File Offset: 0x00013444
		private void OnEnable()
		{
			this.InitMaterial();
			this.m_OldMaterial = this.m_Material;
			DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalAdd;
			if (decalProjectorAction == null)
			{
				return;
			}
			decalProjectorAction(this);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00015268 File Offset: 0x00013468
		private void OnDisable()
		{
			DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalRemove;
			if (decalProjectorAction == null)
			{
				return;
			}
			decalProjectorAction(this);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001527C File Offset: 0x0001347C
		internal void OnValidate()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this.m_Material != this.m_OldMaterial)
			{
				DecalProjector.DecalProjectorAction decalProjectorAction = DecalProjector.onDecalMaterialChange;
				if (decalProjectorAction != null)
				{
					decalProjectorAction(this);
				}
				this.m_OldMaterial = this.m_Material;
				return;
			}
			DecalProjector.DecalProjectorAction decalProjectorAction2 = DecalProjector.onDecalPropertyChange;
			if (decalProjectorAction2 == null)
			{
				return;
			}
			decalProjectorAction2(this);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000152D4 File Offset: 0x000134D4
		public bool IsValid()
		{
			return !(this.material == null) && (this.material.FindPass("DBufferProjector") != -1 || this.material.FindPass("DecalProjectorForwardEmissive") != -1 || this.material.FindPass("DecalScreenSpaceProjector") != -1 || this.material.FindPass("DecalGBufferProjector") != -1);
		}

		// Token: 0x04000289 RID: 649
		[SerializeField]
		private Material m_Material;

		// Token: 0x0400028A RID: 650
		[SerializeField]
		private float m_DrawDistance = 1000f;

		// Token: 0x0400028B RID: 651
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeScale = 0.9f;

		// Token: 0x0400028C RID: 652
		[SerializeField]
		[Range(0f, 180f)]
		private float m_StartAngleFade = 180f;

		// Token: 0x0400028D RID: 653
		[SerializeField]
		[Range(0f, 180f)]
		private float m_EndAngleFade = 180f;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		private Vector2 m_UVScale = new Vector2(1f, 1f);

		// Token: 0x0400028F RID: 655
		[SerializeField]
		private Vector2 m_UVBias = new Vector2(0f, 0f);

		// Token: 0x04000290 RID: 656
		[SerializeField]
		private DecalScaleMode m_ScaleMode;

		// Token: 0x04000291 RID: 657
		[SerializeField]
		internal Vector3 m_Offset = new Vector3(0f, 0f, 0.5f);

		// Token: 0x04000292 RID: 658
		[SerializeField]
		internal Vector3 m_Size = new Vector3(1f, 1f, 1f);

		// Token: 0x04000293 RID: 659
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeFactor = 1f;

		// Token: 0x04000294 RID: 660
		private Material m_OldMaterial;

		// Token: 0x02000165 RID: 357
		// (Invoke) Token: 0x0600098C RID: 2444
		internal delegate void DecalProjectorAction(DecalProjector decalProjector);
	}
}
