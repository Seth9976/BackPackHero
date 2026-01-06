using System;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000021 RID: 33
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	[AddComponentMenu("Rendering/2D/Light 2D")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DLightProperties.html")]
	public sealed class Light2D : Light2DBase, ISerializationCallbackReceiver
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000B7F1 File Offset: 0x000099F1
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000B7F9 File Offset: 0x000099F9
		internal LightUtility.LightMeshVertex[] vertices
		{
			get
			{
				return this.m_Vertices;
			}
			set
			{
				this.m_Vertices = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000B802 File Offset: 0x00009A02
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000B80A File Offset: 0x00009A0A
		internal ushort[] indices
		{
			get
			{
				return this.m_Triangles;
			}
			set
			{
				this.m_Triangles = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000B813 File Offset: 0x00009A13
		internal int[] affectedSortingLayers
		{
			get
			{
				return this.m_ApplyToSortingLayers;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000B81B File Offset: 0x00009A1B
		private int lightCookieSpriteInstanceID
		{
			get
			{
				Sprite lightCookieSprite = this.m_LightCookieSprite;
				if (lightCookieSprite == null)
				{
					return 0;
				}
				return lightCookieSprite.GetInstanceID();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000B82E File Offset: 0x00009A2E
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000B836 File Offset: 0x00009A36
		internal BoundingSphere boundingSphere { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000B83F File Offset: 0x00009A3F
		internal Mesh lightMesh
		{
			get
			{
				if (null == this.m_Mesh)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000B860 File Offset: 0x00009A60
		internal bool hasCachedMesh
		{
			get
			{
				return this.vertices.Length > 1 && this.indices.Length > 1;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000B87A File Offset: 0x00009A7A
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000B882 File Offset: 0x00009A82
		public Light2D.LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				if (this.m_LightType != value)
				{
					this.UpdateMesh(false);
				}
				this.m_LightType = value;
				Light2DManager.ErrorIfDuplicateGlobalLight(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		public int blendStyleIndex
		{
			get
			{
				return this.m_BlendStyleIndex;
			}
			set
			{
				this.m_BlendStyleIndex = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000B8B2 File Offset: 0x00009AB2
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000B8BA File Offset: 0x00009ABA
		public float shadowIntensity
		{
			get
			{
				return this.m_ShadowIntensity;
			}
			set
			{
				this.m_ShadowIntensity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		public bool shadowsEnabled
		{
			get
			{
				return this.m_ShadowIntensityEnabled;
			}
			set
			{
				this.m_ShadowIntensityEnabled = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000B8D9 File Offset: 0x00009AD9
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000B8E1 File Offset: 0x00009AE1
		public float shadowVolumeIntensity
		{
			get
			{
				return this.m_ShadowVolumeIntensity;
			}
			set
			{
				this.m_ShadowVolumeIntensity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000B8EF File Offset: 0x00009AEF
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000B8F7 File Offset: 0x00009AF7
		public bool volumetricShadowsEnabled
		{
			get
			{
				return this.m_ShadowVolumeIntensityEnabled;
			}
			set
			{
				this.m_ShadowVolumeIntensityEnabled = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000B900 File Offset: 0x00009B00
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000B908 File Offset: 0x00009B08
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000B911 File Offset: 0x00009B11
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000B919 File Offset: 0x00009B19
		public float intensity
		{
			get
			{
				return this.m_Intensity;
			}
			set
			{
				this.m_Intensity = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000B922 File Offset: 0x00009B22
		[Obsolete]
		public float volumeOpacity
		{
			get
			{
				return this.m_LightVolumeIntensity;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000B92A File Offset: 0x00009B2A
		public float volumeIntensity
		{
			get
			{
				return this.m_LightVolumeIntensity;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000B932 File Offset: 0x00009B32
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000B93A File Offset: 0x00009B3A
		public bool volumeIntensityEnabled
		{
			get
			{
				return this.m_LightVolumeIntensityEnabled;
			}
			set
			{
				this.m_LightVolumeIntensityEnabled = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000B943 File Offset: 0x00009B43
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000B95B File Offset: 0x00009B5B
		public Sprite lightCookieSprite
		{
			get
			{
				if (this.m_LightType == Light2D.LightType.Point)
				{
					return this.m_DeprecatedPointLightCookieSprite;
				}
				return this.m_LightCookieSprite;
			}
			set
			{
				this.m_LightCookieSprite = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000B964 File Offset: 0x00009B64
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000B96C File Offset: 0x00009B6C
		public float falloffIntensity
		{
			get
			{
				return this.m_FalloffIntensity;
			}
			set
			{
				this.m_FalloffIntensity = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000B984 File Offset: 0x00009B84
		[Obsolete]
		public bool alphaBlendOnOverlap
		{
			get
			{
				return this.m_OverlapOperation == Light2D.OverlapOperation.AlphaBlend;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000B98F File Offset: 0x00009B8F
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000B997 File Offset: 0x00009B97
		public Light2D.OverlapOperation overlapOperation
		{
			get
			{
				return this.m_OverlapOperation;
			}
			set
			{
				this.m_OverlapOperation = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		public int lightOrder
		{
			get
			{
				return this.m_LightOrder;
			}
			set
			{
				this.m_LightOrder = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000B9B1 File Offset: 0x00009BB1
		public float normalMapDistance
		{
			get
			{
				return this.m_NormalMapDistance;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		public Light2D.NormalMapQuality normalMapQuality
		{
			get
			{
				return this.m_NormalMapQuality;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000B9C1 File Offset: 0x00009BC1
		public bool renderVolumetricShadows
		{
			get
			{
				return this.volumetricShadowsEnabled && this.shadowVolumeIntensity > 0f;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000B9DA File Offset: 0x00009BDA
		internal void MarkForUpdate()
		{
			this.forceUpdate = true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B9E3 File Offset: 0x00009BE3
		internal void CacheValues()
		{
			this.m_CachedPosition = base.transform.position;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		internal int GetTopMostLitLayer()
		{
			int num = int.MinValue;
			int num2 = 0;
			SortingLayer[] cachedSortingLayer = Light2DManager.GetCachedSortingLayer();
			for (int i = 0; i < this.m_ApplyToSortingLayers.Length; i++)
			{
				for (int j = cachedSortingLayer.Length - 1; j >= num2; j--)
				{
					if (cachedSortingLayer[j].id == this.m_ApplyToSortingLayers[i])
					{
						num = cachedSortingLayer[j].value;
						num2 = j;
					}
				}
			}
			return num;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000BA64 File Offset: 0x00009C64
		internal Bounds UpdateSpriteMesh()
		{
			if (this.m_LightCookieSprite == null && (this.m_Vertices.Length != 1 || this.m_Triangles.Length != 1))
			{
				this.m_Vertices = new LightUtility.LightMeshVertex[1];
				this.m_Triangles = new ushort[1];
			}
			return LightUtility.GenerateSpriteMesh(this, this.m_LightCookieSprite);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000BABC File Offset: 0x00009CBC
		internal void UpdateMesh(bool forceUpdate = false)
		{
			int shapePathHash = LightUtility.GetShapePathHash(this.shapePath);
			bool flag = LightUtility.CheckForChange(this.m_ShapeLightFalloffSize, ref this.m_PreviousShapeLightFalloffSize);
			bool flag2 = LightUtility.CheckForChange(this.m_ShapeLightParametricRadius, ref this.m_PreviousShapeLightParametricRadius);
			bool flag3 = LightUtility.CheckForChange(this.m_ShapeLightParametricSides, ref this.m_PreviousShapeLightParametricSides);
			bool flag4 = LightUtility.CheckForChange(this.m_ShapeLightParametricAngleOffset, ref this.m_PreviousShapeLightParametricAngleOffset);
			bool flag5 = LightUtility.CheckForChange(this.lightCookieSpriteInstanceID, ref this.m_PreviousLightCookieSprite);
			bool flag6 = LightUtility.CheckForChange(shapePathHash, ref this.m_PreviousShapePathHash);
			bool flag7 = LightUtility.CheckForChange(this.m_LightType, ref this.m_PreviousLightType);
			if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || forceUpdate)
			{
				switch (this.m_LightType)
				{
				case Light2D.LightType.Parametric:
					this.m_LocalBounds = LightUtility.GenerateParametricMesh(this, this.m_ShapeLightParametricRadius, this.m_ShapeLightFalloffSize, this.m_ShapeLightParametricAngleOffset, this.m_ShapeLightParametricSides);
					return;
				case Light2D.LightType.Freeform:
					this.m_LocalBounds = LightUtility.GenerateShapeMesh(this, this.m_ShapePath, this.m_ShapeLightFalloffSize);
					return;
				case Light2D.LightType.Sprite:
					this.m_LocalBounds = this.UpdateSpriteMesh();
					return;
				case Light2D.LightType.Point:
					this.m_LocalBounds = LightUtility.GenerateParametricMesh(this, 1.412135f, 0f, 0f, 4);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		internal void UpdateBoundingSphere()
		{
			if (this.isPointLight)
			{
				this.boundingSphere = new BoundingSphere(base.transform.position, this.m_PointLightOuterRadius);
				return;
			}
			Vector3 vector = base.transform.TransformPoint(Vector3.Max(this.m_LocalBounds.max, this.m_LocalBounds.max + this.m_ShapeLightFalloffOffset));
			Vector3 vector2 = base.transform.TransformPoint(Vector3.Min(this.m_LocalBounds.min, this.m_LocalBounds.min + this.m_ShapeLightFalloffOffset));
			Vector3 vector3 = 0.5f * (vector + vector2);
			float num = Vector3.Magnitude(vector - vector3);
			this.boundingSphere = new BoundingSphere(vector3, num);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000BCBC File Offset: 0x00009EBC
		internal bool IsLitLayer(int layer)
		{
			if (this.m_ApplyToSortingLayers == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_ApplyToSortingLayers.Length; i++)
			{
				if (this.m_ApplyToSortingLayers[i] == layer)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		private void Awake()
		{
			if (this.m_LightCookieSprite != null)
			{
				bool flag = !this.hasCachedMesh || (this.m_LightType == Light2D.LightType.Sprite && this.m_LightCookieSprite.packed);
				this.UpdateMesh(flag);
				if (this.hasCachedMesh)
				{
					this.lightMesh.SetVertexBufferParams(this.vertices.Length, LightUtility.LightMeshVertex.VertexLayout);
					this.lightMesh.SetVertexBufferData<LightUtility.LightMeshVertex>(this.vertices, 0, 0, this.vertices.Length, 0, MeshUpdateFlags.Default);
					this.lightMesh.SetIndices(this.indices, MeshTopology.Triangles, 0, false, 0);
				}
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000BD8B File Offset: 0x00009F8B
		private void OnEnable()
		{
			this.m_PreviousLightCookieSprite = this.lightCookieSpriteInstanceID;
			Light2DManager.RegisterLight(this);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000BD9F File Offset: 0x00009F9F
		private void OnDisable()
		{
			Light2DManager.DeregisterLight(this);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000BDA7 File Offset: 0x00009FA7
		private void LateUpdate()
		{
			if (this.m_LightType == Light2D.LightType.Global)
			{
				return;
			}
			this.UpdateMesh(this.forceUpdate);
			this.UpdateBoundingSphere();
			this.forceUpdate = false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public void OnBeforeSerialize()
		{
			this.m_ComponentVersion = Light2D.ComponentVersions.Version_1;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		public void OnAfterDeserialize()
		{
			if (this.m_ComponentVersion == Light2D.ComponentVersions.Version_Unserialized)
			{
				this.m_ShadowVolumeIntensityEnabled = this.m_ShadowVolumeIntensity > 0f;
				this.m_ShadowIntensityEnabled = this.m_ShadowIntensity > 0f;
				this.m_LightVolumeIntensityEnabled = this.m_LightVolumeIntensity > 0f;
				this.m_NormalMapQuality = ((!this.m_UseNormalMap) ? Light2D.NormalMapQuality.Disabled : this.m_NormalMapQuality);
				this.m_OverlapOperation = (this.m_AlphaBlendOnOverlap ? Light2D.OverlapOperation.AlphaBlend : this.m_OverlapOperation);
				this.m_ComponentVersion = Light2D.ComponentVersions.Version_1;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000BE5B File Offset: 0x0000A05B
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000BE63 File Offset: 0x0000A063
		public float pointLightInnerAngle
		{
			get
			{
				return this.m_PointLightInnerAngle;
			}
			set
			{
				this.m_PointLightInnerAngle = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000BE6C File Offset: 0x0000A06C
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000BE74 File Offset: 0x0000A074
		public float pointLightOuterAngle
		{
			get
			{
				return this.m_PointLightOuterAngle;
			}
			set
			{
				this.m_PointLightOuterAngle = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000BE7D File Offset: 0x0000A07D
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000BE85 File Offset: 0x0000A085
		public float pointLightInnerRadius
		{
			get
			{
				return this.m_PointLightInnerRadius;
			}
			set
			{
				this.m_PointLightInnerRadius = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000BE8E File Offset: 0x0000A08E
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000BE96 File Offset: 0x0000A096
		public float pointLightOuterRadius
		{
			get
			{
				return this.m_PointLightOuterRadius;
			}
			set
			{
				this.m_PointLightOuterRadius = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000BE9F File Offset: 0x0000A09F
		[Obsolete("pointLightDistance has been changed to normalMapDistance", true)]
		public float pointLightDistance
		{
			get
			{
				return this.m_NormalMapDistance;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		[Obsolete("pointLightQuality has been changed to normalMapQuality", true)]
		public Light2D.NormalMapQuality pointLightQuality
		{
			get
			{
				return this.m_NormalMapQuality;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000BEAF File Offset: 0x0000A0AF
		internal bool isPointLight
		{
			get
			{
				return this.m_LightType == Light2D.LightType.Point;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000BEBA File Offset: 0x0000A0BA
		public int shapeLightParametricSides
		{
			get
			{
				return this.m_ShapeLightParametricSides;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000BEC2 File Offset: 0x0000A0C2
		public float shapeLightParametricAngleOffset
		{
			get
			{
				return this.m_ShapeLightParametricAngleOffset;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000BECA File Offset: 0x0000A0CA
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000BED2 File Offset: 0x0000A0D2
		public float shapeLightParametricRadius
		{
			get
			{
				return this.m_ShapeLightParametricRadius;
			}
			internal set
			{
				this.m_ShapeLightParametricRadius = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000BEDB File Offset: 0x0000A0DB
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000BEE3 File Offset: 0x0000A0E3
		public float shapeLightFalloffSize
		{
			get
			{
				return this.m_ShapeLightFalloffSize;
			}
			set
			{
				this.m_ShapeLightFalloffSize = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000BEF6 File Offset: 0x0000A0F6
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		public Vector3[] shapePath
		{
			get
			{
				return this.m_ShapePath;
			}
			internal set
			{
				this.m_ShapePath = value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000BF07 File Offset: 0x0000A107
		public void SetShapePath(Vector3[] path)
		{
			this.m_ShapePath = path;
		}

		// Token: 0x0400009F RID: 159
		private const Light2D.ComponentVersions k_CurrentComponentVersion = Light2D.ComponentVersions.Version_1;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private Light2D.ComponentVersions m_ComponentVersion;

		// Token: 0x040000A1 RID: 161
		[NotKeyable]
		[SerializeField]
		private Light2D.LightType m_LightType = Light2D.LightType.Point;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		[FormerlySerializedAs("m_LightOperationIndex")]
		private int m_BlendStyleIndex;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private float m_FalloffIntensity = 0.5f;

		// Token: 0x040000A4 RID: 164
		[ColorUsage(true)]
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private float m_Intensity = 1f;

		// Token: 0x040000A6 RID: 166
		[FormerlySerializedAs("m_LightVolumeOpacity")]
		[SerializeField]
		private float m_LightVolumeIntensity = 1f;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private bool m_LightVolumeIntensityEnabled;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private int[] m_ApplyToSortingLayers = new int[1];

		// Token: 0x040000A9 RID: 169
		[Reload("Textures/2D/Sparkle.png", ReloadAttribute.Package.Root)]
		[SerializeField]
		private Sprite m_LightCookieSprite;

		// Token: 0x040000AA RID: 170
		[FormerlySerializedAs("m_LightCookieSprite")]
		[SerializeField]
		private Sprite m_DeprecatedPointLightCookieSprite;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private int m_LightOrder;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private bool m_AlphaBlendOnOverlap;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private Light2D.OverlapOperation m_OverlapOperation;

		// Token: 0x040000AE RID: 174
		[FormerlySerializedAs("m_PointLightDistance")]
		[SerializeField]
		private float m_NormalMapDistance = 3f;

		// Token: 0x040000AF RID: 175
		[NotKeyable]
		[FormerlySerializedAs("m_PointLightQuality")]
		[SerializeField]
		private Light2D.NormalMapQuality m_NormalMapQuality = Light2D.NormalMapQuality.Disabled;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private bool m_UseNormalMap;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private bool m_ShadowIntensityEnabled;

		// Token: 0x040000B2 RID: 178
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowIntensity = 0.75f;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private bool m_ShadowVolumeIntensityEnabled;

		// Token: 0x040000B4 RID: 180
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowVolumeIntensity = 0.75f;

		// Token: 0x040000B5 RID: 181
		private Mesh m_Mesh;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private LightUtility.LightMeshVertex[] m_Vertices = new LightUtility.LightMeshVertex[1];

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private ushort[] m_Triangles = new ushort[1];

		// Token: 0x040000B8 RID: 184
		private int m_PreviousLightCookieSprite;

		// Token: 0x040000B9 RID: 185
		internal Vector3 m_CachedPosition;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private Bounds m_LocalBounds;

		// Token: 0x040000BC RID: 188
		internal bool forceUpdate;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private float m_PointLightInnerAngle = 360f;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private float m_PointLightOuterAngle = 360f;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private float m_PointLightInnerRadius;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		private float m_PointLightOuterRadius = 1f;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		private int m_ShapeLightParametricSides = 5;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		private float m_ShapeLightParametricAngleOffset;

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private float m_ShapeLightParametricRadius = 1f;

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		private float m_ShapeLightFalloffSize = 0.5f;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private Vector2 m_ShapeLightFalloffOffset = Vector2.zero;

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		private Vector3[] m_ShapePath;

		// Token: 0x040000C7 RID: 199
		private float m_PreviousShapeLightFalloffSize = -1f;

		// Token: 0x040000C8 RID: 200
		private int m_PreviousShapeLightParametricSides = -1;

		// Token: 0x040000C9 RID: 201
		private float m_PreviousShapeLightParametricAngleOffset = -1f;

		// Token: 0x040000CA RID: 202
		private float m_PreviousShapeLightParametricRadius = -1f;

		// Token: 0x040000CB RID: 203
		private int m_PreviousShapePathHash = -1;

		// Token: 0x040000CC RID: 204
		private Light2D.LightType m_PreviousLightType;

		// Token: 0x0200013A RID: 314
		public enum DeprecatedLightType
		{
			// Token: 0x04000889 RID: 2185
			Parametric
		}

		// Token: 0x0200013B RID: 315
		public enum LightType
		{
			// Token: 0x0400088B RID: 2187
			Parametric,
			// Token: 0x0400088C RID: 2188
			Freeform,
			// Token: 0x0400088D RID: 2189
			Sprite,
			// Token: 0x0400088E RID: 2190
			Point,
			// Token: 0x0400088F RID: 2191
			Global
		}

		// Token: 0x0200013C RID: 316
		public enum NormalMapQuality
		{
			// Token: 0x04000891 RID: 2193
			Disabled = 2,
			// Token: 0x04000892 RID: 2194
			Fast = 0,
			// Token: 0x04000893 RID: 2195
			Accurate
		}

		// Token: 0x0200013D RID: 317
		public enum OverlapOperation
		{
			// Token: 0x04000895 RID: 2197
			Additive,
			// Token: 0x04000896 RID: 2198
			AlphaBlend
		}

		// Token: 0x0200013E RID: 318
		private enum ComponentVersions
		{
			// Token: 0x04000898 RID: 2200
			Version_Unserialized,
			// Token: 0x04000899 RID: 2201
			Version_1
		}
	}
}
