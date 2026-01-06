using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	// Token: 0x0200001F RID: 31
	[ExecuteInEditMode]
	[RequireComponent(typeof(SpriteShapeRenderer))]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@latest/index.html?subfolder=/manual/SSController.html")]
	public class SpriteShapeController : MonoBehaviour
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00007791 File Offset: 0x00005991
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00007799 File Offset: 0x00005999
		internal int maxArrayCount
		{
			get
			{
				return this.m_MaxArrayCount;
			}
			set
			{
				this.m_MaxArrayCount = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000077A2 File Offset: 0x000059A2
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000077AA File Offset: 0x000059AA
		internal bool geometryCached
		{
			get
			{
				return this.m_GeometryCached;
			}
			set
			{
				this.m_GeometryCached = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000077B3 File Offset: 0x000059B3
		internal int splineHashCode
		{
			get
			{
				return this.m_ActiveSplineHash;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000077BB File Offset: 0x000059BB
		internal Sprite[] spriteArray
		{
			get
			{
				return this.m_SpriteArray;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000077C3 File Offset: 0x000059C3
		internal SpriteShapeParameters spriteShapeParameters
		{
			get
			{
				return this.m_ActiveShapeParameters;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000077CC File Offset: 0x000059CC
		internal SpriteShapeGeometryCache spriteShapeGeometryCache
		{
			get
			{
				if (!this.m_SpriteShapeGeometryCache)
				{
					Component component;
					this.m_SpriteShapeGeometryCache = (base.TryGetComponent(typeof(SpriteShapeGeometryCache), out component) ? (component as SpriteShapeGeometryCache) : null);
				}
				return this.m_SpriteShapeGeometryCache;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007811 File Offset: 0x00005A11
		public int spriteShapeHashCode
		{
			get
			{
				return this.m_ActiveSpriteShapeHash;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007819 File Offset: 0x00005A19
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00007821 File Offset: 0x00005A21
		public bool worldSpaceUVs
		{
			get
			{
				return this.m_WorldSpaceUV;
			}
			set
			{
				this.m_WorldSpaceUV = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000782A File Offset: 0x00005A2A
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00007832 File Offset: 0x00005A32
		public float fillPixelsPerUnit
		{
			get
			{
				return this.m_FillPixelPerUnit;
			}
			set
			{
				this.m_FillPixelPerUnit = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000783B File Offset: 0x00005A3B
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007843 File Offset: 0x00005A43
		public bool enableTangents
		{
			get
			{
				return this.m_EnableTangents;
			}
			set
			{
				this.m_EnableTangents = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000784C File Offset: 0x00005A4C
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007854 File Offset: 0x00005A54
		public float stretchTiling
		{
			get
			{
				return this.m_StretchTiling;
			}
			set
			{
				this.m_StretchTiling = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000785D File Offset: 0x00005A5D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00007865 File Offset: 0x00005A65
		public int splineDetail
		{
			get
			{
				return this.m_SplineDetail;
			}
			set
			{
				this.m_SplineDetail = Mathf.Max(0, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007874 File Offset: 0x00005A74
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000787C File Offset: 0x00005A7C
		public int colliderDetail
		{
			get
			{
				return this.m_ColliderDetail;
			}
			set
			{
				this.m_ColliderDetail = Mathf.Max(0, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000788B File Offset: 0x00005A8B
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00007893 File Offset: 0x00005A93
		public float colliderOffset
		{
			get
			{
				return this.m_ColliderOffset;
			}
			set
			{
				this.m_ColliderOffset = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000789C File Offset: 0x00005A9C
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000078A4 File Offset: 0x00005AA4
		public float cornerAngleThreshold
		{
			get
			{
				return this.m_CornerAngleThreshold;
			}
			set
			{
				this.m_CornerAngleThreshold = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000078AD File Offset: 0x00005AAD
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000078B5 File Offset: 0x00005AB5
		public bool autoUpdateCollider
		{
			get
			{
				return this.m_UpdateCollider;
			}
			set
			{
				this.m_UpdateCollider = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000078BE File Offset: 0x00005ABE
		public bool optimizeCollider
		{
			get
			{
				return this.m_OptimizeCollider;
			}
		}

		// Token: 0x1700002D RID: 45
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000078C6 File Offset: 0x00005AC6
		internal bool optimizeColliderInternal
		{
			set
			{
				this.m_OptimizeCollider = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000078CF File Offset: 0x00005ACF
		public bool optimizeGeometry
		{
			get
			{
				return this.m_OptimizeGeometry;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000078D7 File Offset: 0x00005AD7
		public bool hasCollider
		{
			get
			{
				return this.edgeCollider != null || this.polygonCollider != null;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000078F5 File Offset: 0x00005AF5
		public Spline spline
		{
			get
			{
				return this.m_Spline;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000078FD File Offset: 0x00005AFD
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00007905 File Offset: 0x00005B05
		public SpriteShape spriteShape
		{
			get
			{
				return this.m_SpriteShape;
			}
			set
			{
				this.m_SpriteShape = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007910 File Offset: 0x00005B10
		public EdgeCollider2D edgeCollider
		{
			get
			{
				if (!this.m_EdgeCollider2D)
				{
					Component component;
					this.m_EdgeCollider2D = (base.TryGetComponent(typeof(EdgeCollider2D), out component) ? (component as EdgeCollider2D) : null);
				}
				return this.m_EdgeCollider2D;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007958 File Offset: 0x00005B58
		public PolygonCollider2D polygonCollider
		{
			get
			{
				if (!this.m_PolygonCollider2D)
				{
					Component component;
					this.m_PolygonCollider2D = (base.TryGetComponent(typeof(PolygonCollider2D), out component) ? (component as PolygonCollider2D) : null);
				}
				return this.m_PolygonCollider2D;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000799D File Offset: 0x00005B9D
		public SpriteShapeRenderer spriteShapeRenderer
		{
			get
			{
				if (!this.m_SpriteShapeRenderer)
				{
					this.m_SpriteShapeRenderer = base.GetComponent<SpriteShapeRenderer>();
				}
				return this.m_SpriteShapeRenderer;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000079BE File Offset: 0x00005BBE
		private void DisposeInternal()
		{
			if (this.m_ColliderData.IsCreated)
			{
				this.m_ColliderData.Dispose();
			}
			if (this.m_TangentData.IsCreated)
			{
				this.m_TangentData.Dispose();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000079F0 File Offset: 0x00005BF0
		private void OnApplicationQuit()
		{
			this.DisposeInternal();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000079F8 File Offset: 0x00005BF8
		private void OnEnable()
		{
			this.m_DynamicOcclusionOverriden = true;
			this.m_DynamicOcclusionLocal = this.spriteShapeRenderer.allowOcclusionWhenDynamic;
			this.spriteShapeRenderer.allowOcclusionWhenDynamic = false;
			this.InitBounds();
			this.UpdateSpriteData();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007A2B File Offset: 0x00005C2B
		private void OnDisable()
		{
			this.DisposeInternal();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007A33 File Offset: 0x00005C33
		private void OnDestroy()
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007A38 File Offset: 0x00005C38
		private void Reset()
		{
			this.m_SplineDetail = 16;
			this.m_AdaptiveUV = true;
			this.m_StretchUV = false;
			this.m_FillPixelPerUnit = 100f;
			this.m_ColliderDetail = 16;
			this.m_StretchTiling = 1f;
			this.m_WorldSpaceUV = false;
			this.m_CornerAngleThreshold = 30f;
			this.m_ColliderOffset = 0f;
			this.m_UpdateCollider = true;
			this.m_OptimizeCollider = true;
			this.m_OptimizeGeometry = true;
			this.m_EnableTangents = false;
			this.spline.Clear();
			this.spline.InsertPointAt(0, Vector2.left + Vector2.down);
			this.spline.InsertPointAt(1, Vector2.left + Vector2.up);
			this.spline.InsertPointAt(2, Vector2.right + Vector2.up);
			this.spline.InsertPointAt(3, Vector2.right + Vector2.down);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007B3D File Offset: 0x00005D3D
		private static void SmartDestroy(Object o)
		{
			if (o == null)
			{
				return;
			}
			Object.Destroy(o);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007B50 File Offset: 0x00005D50
		internal Bounds InitBounds()
		{
			int pointCount = this.spline.GetPointCount();
			if (pointCount > 1)
			{
				Bounds bounds = new Bounds(this.spline.GetPosition(0), Vector3.zero);
				for (int i = 1; i < pointCount; i++)
				{
					bounds.Encapsulate(this.spline.GetPosition(i));
				}
				bounds.Encapsulate(this.spriteShapeRenderer.localBounds);
				this.spriteShapeRenderer.SetLocalAABB(bounds);
				return bounds;
			}
			return default(Bounds);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007BCD File Offset: 0x00005DCD
		public void RefreshSpriteShape()
		{
			this.m_ActiveSplineHash = 0;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007BD8 File Offset: 0x00005DD8
		private bool ValidateSpline()
		{
			int pointCount = this.spline.GetPointCount();
			if (pointCount < 2)
			{
				return false;
			}
			for (int i = 0; i < pointCount - 1; i++)
			{
				if ((this.spline.GetPosition(i) - this.spline.GetPosition(i + 1)).sqrMagnitude < 0.001f)
				{
					Debug.LogWarningFormat(base.gameObject, "[SpriteShape] Control points {0} & {1} are too close. SpriteShape will not be generated for < {2} >.", new object[]
					{
						i,
						i + 1,
						base.gameObject.name
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007C70 File Offset: 0x00005E70
		private bool ValidateSpriteShapeTexture()
		{
			bool flag = false;
			if (this.spriteShape != null)
			{
				if (!this.spline.isOpenEnded)
				{
					flag = this.spriteShape.fillTexture != null;
				}
			}
			else
			{
				Debug.LogWarningFormat(base.gameObject, "[SpriteShape] A valid SpriteShape profile has not been set for gameObject < {0} >.", new object[] { base.gameObject.name });
			}
			return flag;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007CD4 File Offset: 0x00005ED4
		internal bool ValidateUTess2D()
		{
			bool flag = this.m_UTess2D;
			if (this.m_UTess2D && null != this.spriteShape)
			{
				flag = this.spriteShape.fillOffset == 0f;
			}
			return flag;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007D12 File Offset: 0x00005F12
		private bool HasSpriteShapeChanged()
		{
			bool flag = this.m_ActiveSpriteShape != this.spriteShape;
			if (flag)
			{
				this.m_ActiveSpriteShape = this.spriteShape;
			}
			return flag;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007D34 File Offset: 0x00005F34
		private bool HasSpriteShapeDataChanged()
		{
			bool flag = this.HasSpriteShapeChanged();
			if (this.spriteShape)
			{
				int spriteShapeHashCode = SpriteShape.GetSpriteShapeHashCode(this.spriteShape);
				if (this.spriteShapeHashCode != spriteShapeHashCode)
				{
					this.m_ActiveSpriteShapeHash = spriteShapeHashCode;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007D74 File Offset: 0x00005F74
		private bool HasSplineDataChanged()
		{
			int num = -2128831035 ^ this.spline.GetHashCode();
			num = (num * 16777619) ^ (this.m_UTess2D ? 1 : 0);
			num = (num * 16777619) ^ (this.m_WorldSpaceUV ? 1 : 0);
			num = (num * 16777619) ^ (this.m_EnableTangents ? 1 : 0);
			num = (num * 16777619) ^ (this.m_GeometryCached ? 1 : 0);
			num = (num * 16777619) ^ (this.m_OptimizeGeometry ? 1 : 0);
			num = (num * 16777619) ^ this.m_StretchTiling.GetHashCode();
			num = (num * 16777619) ^ this.m_ColliderOffset.GetHashCode();
			num = (num * 16777619) ^ this.m_ColliderDetail.GetHashCode();
			if (this.splineHashCode != num)
			{
				this.m_ActiveSplineHash = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007E4B File Offset: 0x0000604B
		private void LateUpdate()
		{
			this.BakeCollider();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007E53 File Offset: 0x00006053
		private void OnWillRenderObject()
		{
			this.BakeMesh();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007E5C File Offset: 0x0000605C
		public JobHandle BakeMesh()
		{
			JobHandle jobHandle = default(JobHandle);
			if (this.spriteShapeGeometryCache && this.m_ActiveSplineHash != 0 && this.spriteShapeGeometryCache.maxArrayCount != 0)
			{
				return jobHandle;
			}
			if (this.ValidateSpline())
			{
				bool flag = this.HasSplineDataChanged();
				bool flag2 = this.HasSpriteShapeDataChanged();
				bool flag3 = this.UpdateSpriteShapeParameters();
				if (flag || flag2 || flag3)
				{
					if (flag2)
					{
						this.UpdateSpriteData();
					}
					jobHandle = this.ScheduleBake();
				}
			}
			return jobHandle;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007EC8 File Offset: 0x000060C8
		internal void UpdateGeometryCache()
		{
			if (this.spriteShapeGeometryCache && this.geometryCached)
			{
				this.m_JobHandle.Complete();
				this.spriteShapeGeometryCache.UpdateGeometryCache();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007EF8 File Offset: 0x000060F8
		public bool UpdateSpriteShapeParameters()
		{
			bool flag = !this.spline.isOpenEnded;
			bool flag2 = true;
			bool adaptiveUV = this.m_AdaptiveUV;
			bool stretchUV = this.m_StretchUV;
			bool flag3 = false;
			uint num = 0U;
			uint splineDetail = (uint)this.m_SplineDetail;
			float num2 = 0f;
			float num3 = ((this.m_CornerAngleThreshold >= 0f && this.m_CornerAngleThreshold < 90f) ? this.m_CornerAngleThreshold : 89.9999f);
			Texture2D texture2D = null;
			Matrix4x4 matrix4x = Matrix4x4.identity;
			if (this.spriteShape)
			{
				if (this.worldSpaceUVs)
				{
					matrix4x = base.transform.localToWorldMatrix;
				}
				texture2D = this.spriteShape.fillTexture;
				num = (stretchUV ? ((uint)this.stretchTiling) : ((uint)this.fillPixelsPerUnit));
				num2 = this.spriteShape.fillOffset;
				flag3 = this.spriteShape.useSpriteBorders;
				if (this.spriteShape.cornerSprites.Count > 0)
				{
					flag2 = false;
				}
			}
			bool flag4 = this.m_ActiveShapeParameters.adaptiveUV != adaptiveUV || this.m_ActiveShapeParameters.angleThreshold != num3 || this.m_ActiveShapeParameters.borderPivot != num2 || this.m_ActiveShapeParameters.carpet != flag || this.m_ActiveShapeParameters.fillScale != num || this.m_ActiveShapeParameters.fillTexture != texture2D || this.m_ActiveShapeParameters.smartSprite != flag2 || this.m_ActiveShapeParameters.splineDetail != splineDetail || this.m_ActiveShapeParameters.spriteBorders != flag3 || this.m_ActiveShapeParameters.transform != matrix4x || this.m_ActiveShapeParameters.stretchUV != stretchUV;
			this.m_ActiveShapeParameters.adaptiveUV = adaptiveUV;
			this.m_ActiveShapeParameters.stretchUV = stretchUV;
			this.m_ActiveShapeParameters.angleThreshold = num3;
			this.m_ActiveShapeParameters.borderPivot = num2;
			this.m_ActiveShapeParameters.carpet = flag;
			this.m_ActiveShapeParameters.fillScale = num;
			this.m_ActiveShapeParameters.fillTexture = texture2D;
			this.m_ActiveShapeParameters.smartSprite = flag2;
			this.m_ActiveShapeParameters.splineDetail = splineDetail;
			this.m_ActiveShapeParameters.spriteBorders = flag3;
			this.m_ActiveShapeParameters.transform = matrix4x;
			return flag4;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008124 File Offset: 0x00006324
		private void UpdateSpriteData()
		{
			if (this.spriteShape)
			{
				List<Sprite> list = new List<Sprite>();
				List<Sprite> list2 = new List<Sprite>();
				List<AngleRangeInfo> list3 = new List<AngleRangeInfo>();
				List<AngleRange> list4 = new List<AngleRange>(this.spriteShape.angleRanges);
				list4.Sort((AngleRange a, AngleRange b) => a.order.CompareTo(b.order));
				for (int i = 0; i < list4.Count; i++)
				{
					bool flag = false;
					AngleRange angleRange = list4[i];
					using (List<Sprite>.Enumerator enumerator = angleRange.sprites.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current != null)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						AngleRangeInfo angleRangeInfo = default(AngleRangeInfo);
						angleRangeInfo.start = angleRange.start;
						angleRangeInfo.end = angleRange.end;
						angleRangeInfo.order = (uint)angleRange.order;
						List<int> list5 = new List<int>();
						foreach (Sprite sprite in angleRange.sprites)
						{
							list.Add(sprite);
							list5.Add(list.Count - 1);
						}
						angleRangeInfo.sprites = list5.ToArray();
						list3.Add(angleRangeInfo);
					}
				}
				bool flag2 = false;
				using (List<CornerSprite>.Enumerator enumerator2 = this.spriteShape.cornerSprites.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.sprites[0] != null)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					for (int j = 0; j < this.spriteShape.cornerSprites.Count; j++)
					{
						CornerSprite cornerSprite = this.spriteShape.cornerSprites[j];
						list2.Add(cornerSprite.sprites[0]);
					}
				}
				this.m_EdgeSpriteArray = list.ToArray();
				this.m_CornerSpriteArray = list2.ToArray();
				this.m_AngleRangeInfoArray = list3.ToArray();
				List<Sprite> list6 = new List<Sprite>();
				list6.AddRange(this.m_EdgeSpriteArray);
				list6.AddRange(this.m_CornerSpriteArray);
				this.m_SpriteArray = list6.ToArray();
				return;
			}
			this.m_SpriteArray = new Sprite[0];
			this.m_EdgeSpriteArray = new Sprite[0];
			this.m_CornerSpriteArray = new Sprite[0];
			this.m_AngleRangeInfoArray = new AngleRangeInfo[0];
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000083D4 File Offset: 0x000065D4
		private NativeArray<ShapeControlPoint> GetShapeControlPoints()
		{
			int pointCount = this.spline.GetPointCount();
			NativeArray<ShapeControlPoint> nativeArray = new NativeArray<ShapeControlPoint>(pointCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < pointCount; i++)
			{
				ShapeControlPoint shapeControlPoint;
				shapeControlPoint.position = this.spline.GetPosition(i);
				shapeControlPoint.leftTangent = this.spline.GetLeftTangent(i);
				shapeControlPoint.rightTangent = this.spline.GetRightTangent(i);
				shapeControlPoint.mode = (int)this.spline.GetTangentMode(i);
				nativeArray[i] = shapeControlPoint;
			}
			return nativeArray;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000845C File Offset: 0x0000665C
		private NativeArray<SplinePointMetaData> GetSplinePointMetaData()
		{
			int pointCount = this.spline.GetPointCount();
			NativeArray<SplinePointMetaData> nativeArray = new NativeArray<SplinePointMetaData>(pointCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < pointCount; i++)
			{
				SplinePointMetaData splinePointMetaData;
				splinePointMetaData.height = this.m_Spline.GetHeight(i);
				splinePointMetaData.spriteIndex = (uint)this.m_Spline.GetSpriteIndex(i);
				splinePointMetaData.cornerMode = (int)this.m_Spline.GetCornerMode(i);
				nativeArray[i] = splinePointMetaData;
			}
			return nativeArray;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000084D0 File Offset: 0x000066D0
		private int CalculateMaxArrayCount(NativeArray<ShapeControlPoint> shapePoints)
		{
			int num = 65536;
			bool flag = false;
			float num2 = 99999f;
			if (this.spriteArray != null)
			{
				foreach (Sprite sprite in this.m_SpriteArray)
				{
					if (sprite != null)
					{
						flag = true;
						float spritePixelWidth = BezierUtility.GetSpritePixelWidth(sprite);
						num2 = ((num2 > spritePixelWidth) ? spritePixelWidth : num2);
					}
				}
			}
			float num3 = num2;
			float num4 = BezierUtility.BezierLength(shapePoints, this.splineDetail, ref num3) * 4f;
			int num5 = shapePoints.Length * 4 * this.splineDetail;
			int num6 = (flag ? ((int)(num4 / num3) * this.splineDetail + num5) : 0);
			num5 = (this.optimizeGeometry ? num5 : (num5 * 2));
			num5 = (this.ValidateSpriteShapeTexture() ? num5 : 0);
			this.maxArrayCount = num5 + num6;
			this.maxArrayCount = math.min(this.maxArrayCount, num);
			return this.maxArrayCount;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000085BC File Offset: 0x000067BC
		private JobHandle ScheduleBake()
		{
			JobHandle jobHandle = default(JobHandle);
			bool isPlaying = Application.isPlaying;
			if (true && this.geometryCached && this.spriteShapeGeometryCache && this.spriteShapeGeometryCache.maxArrayCount != 0)
			{
				return this.spriteShapeGeometryCache.Upload(this.spriteShapeRenderer, this);
			}
			this.spline.GetPointCount();
			NativeArray<ShapeControlPoint> shapeControlPoints = this.GetShapeControlPoints();
			NativeArray<SplinePointMetaData> splinePointMetaData = this.GetSplinePointMetaData();
			this.maxArrayCount = this.CalculateMaxArrayCount(shapeControlPoints);
			if (this.maxArrayCount > 0 && base.enabled)
			{
				this.m_JobHandle.Complete();
				if (this.m_ColliderData.IsCreated)
				{
					this.m_ColliderData.Dispose();
				}
				this.m_ColliderData = new NativeArray<float2>(this.maxArrayCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				if (!this.m_TangentData.IsCreated)
				{
					this.m_TangentData = new NativeArray<Vector4>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				}
				NativeArray<Bounds> bounds = this.spriteShapeRenderer.GetBounds();
				NativeArray<SpriteShapeSegment> segments = this.spriteShapeRenderer.GetSegments(shapeControlPoints.Length * 8);
				NativeSlice<Vector4> nativeSlice = new NativeSlice<Vector4>(this.m_TangentData);
				NativeArray<ushort> nativeArray;
				NativeSlice<Vector3> nativeSlice2;
				NativeSlice<Vector2> nativeSlice3;
				if (this.m_EnableTangents)
				{
					this.spriteShapeRenderer.GetChannels(this.maxArrayCount, out nativeArray, out nativeSlice2, out nativeSlice3, out nativeSlice);
				}
				else
				{
					this.spriteShapeRenderer.GetChannels(this.maxArrayCount, out nativeArray, out nativeSlice2, out nativeSlice3);
				}
				bool flag = this.ValidateUTess2D();
				SpriteShapeGenerator spriteShapeGenerator = new SpriteShapeGenerator
				{
					m_Bounds = bounds,
					m_PosArray = nativeSlice2,
					m_Uv0Array = nativeSlice3,
					m_TanArray = nativeSlice,
					m_GeomArray = segments,
					m_IndexArray = nativeArray,
					m_ColliderPoints = this.m_ColliderData
				};
				spriteShapeGenerator.generateCollider = SpriteShapeController.generateCollider;
				spriteShapeGenerator.generateGeometry = SpriteShapeController.generateGeometry;
				spriteShapeGenerator.Prepare(this, this.m_ActiveShapeParameters, this.maxArrayCount, shapeControlPoints, splinePointMetaData, this.m_AngleRangeInfoArray, this.m_EdgeSpriteArray, this.m_CornerSpriteArray, flag);
				this.m_JobHandle = spriteShapeGenerator.Schedule(default(JobHandle));
				this.spriteShapeRenderer.Prepare(this.m_JobHandle, this.m_ActiveShapeParameters, this.m_SpriteArray);
				jobHandle = this.m_JobHandle;
				JobHandle.ScheduleBatchedJobs();
			}
			if (this.m_DynamicOcclusionOverriden)
			{
				this.spriteShapeRenderer.allowOcclusionWhenDynamic = this.m_DynamicOcclusionLocal;
				this.m_DynamicOcclusionOverriden = false;
			}
			shapeControlPoints.Dispose();
			splinePointMetaData.Dispose();
			return jobHandle;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008810 File Offset: 0x00006A10
		public void BakeCollider()
		{
			this.m_JobHandle.Complete();
			if (this.m_ColliderData.IsCreated)
			{
				if (this.autoUpdateCollider && this.hasCollider)
				{
					int num = 32766;
					float2 @float = 0;
					List<Vector2> list = new List<Vector2>();
					for (int i = 0; i < num; i++)
					{
						float2 float2 = this.m_ColliderData[i];
						if (!math.any(@float) && !math.any(float2))
						{
							if (i + 1 >= num)
							{
								break;
							}
							float2 float3 = this.m_ColliderData[i + 1];
							if (!math.any(float3) && !math.any(float3))
							{
								break;
							}
						}
						list.Add(new Vector2(float2.x, float2.y));
					}
					if (this.edgeCollider != null)
					{
						this.edgeCollider.points = list.ToArray();
					}
					if (this.polygonCollider != null)
					{
						this.polygonCollider.points = list.ToArray();
					}
				}
				this.m_ColliderData.Dispose();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008918 File Offset: 0x00006B18
		internal void BakeMeshForced()
		{
			if (this.spriteShapeRenderer != null && this.HasSplineDataChanged())
			{
				this.BakeMesh();
				CommandBuffer commandBuffer = new CommandBuffer();
				commandBuffer.GetTemporaryRT(0, 256, 256, 0);
				commandBuffer.SetRenderTarget(0);
				commandBuffer.DrawRenderer(this.spriteShapeRenderer, this.spriteShapeRenderer.sharedMaterial);
				commandBuffer.ReleaseTemporaryRT(0);
				Graphics.ExecuteCommandBuffer(commandBuffer);
			}
		}

		// Token: 0x04000085 RID: 133
		private const float s_DistanceTolerance = 0.001f;

		// Token: 0x04000086 RID: 134
		private SpriteShape m_ActiveSpriteShape;

		// Token: 0x04000087 RID: 135
		private EdgeCollider2D m_EdgeCollider2D;

		// Token: 0x04000088 RID: 136
		private PolygonCollider2D m_PolygonCollider2D;

		// Token: 0x04000089 RID: 137
		private SpriteShapeRenderer m_SpriteShapeRenderer;

		// Token: 0x0400008A RID: 138
		private SpriteShapeGeometryCache m_SpriteShapeGeometryCache;

		// Token: 0x0400008B RID: 139
		private Sprite[] m_SpriteArray = new Sprite[0];

		// Token: 0x0400008C RID: 140
		private Sprite[] m_EdgeSpriteArray = new Sprite[0];

		// Token: 0x0400008D RID: 141
		private Sprite[] m_CornerSpriteArray = new Sprite[0];

		// Token: 0x0400008E RID: 142
		private AngleRangeInfo[] m_AngleRangeInfoArray = new AngleRangeInfo[0];

		// Token: 0x0400008F RID: 143
		private NativeArray<float2> m_ColliderData;

		// Token: 0x04000090 RID: 144
		private NativeArray<Vector4> m_TangentData;

		// Token: 0x04000091 RID: 145
		private bool m_DynamicOcclusionLocal;

		// Token: 0x04000092 RID: 146
		private bool m_DynamicOcclusionOverriden;

		// Token: 0x04000093 RID: 147
		private int m_ActiveSplineHash;

		// Token: 0x04000094 RID: 148
		private int m_ActiveSpriteShapeHash;

		// Token: 0x04000095 RID: 149
		private int m_MaxArrayCount;

		// Token: 0x04000096 RID: 150
		private JobHandle m_JobHandle;

		// Token: 0x04000097 RID: 151
		private SpriteShapeParameters m_ActiveShapeParameters;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		private Spline m_Spline = new Spline();

		// Token: 0x04000099 RID: 153
		[SerializeField]
		private SpriteShape m_SpriteShape;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		private float m_FillPixelPerUnit = 100f;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private float m_StretchTiling = 1f;

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private int m_SplineDetail;

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private bool m_AdaptiveUV;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		private bool m_StretchUV;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private bool m_WorldSpaceUV;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private float m_CornerAngleThreshold = 30f;

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private int m_ColliderDetail;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_ColliderOffset;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private bool m_UpdateCollider = true;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private bool m_OptimizeCollider = true;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private bool m_OptimizeGeometry = true;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private bool m_EnableTangents;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		[HideInInspector]
		private bool m_GeometryCached;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private bool m_UTess2D;

		// Token: 0x040000A9 RID: 169
		private static readonly ProfilerMarker generateGeometry = new ProfilerMarker("SpriteShape.GenerateGeometry");

		// Token: 0x040000AA RID: 170
		private static readonly ProfilerMarker generateCollider = new ProfilerMarker("SpriteShape.GenerateCollider");
	}
}
