using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000032 RID: 50
	[ReloadGroup]
	[ExcludeFromPreset]
	[MovedFrom("UnityEngine.Experimental.Rendering.Universal")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DRendererData_overview.html")]
	[Serializable]
	public class Renderer2DData : ScriptableRendererData
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00010858 File Offset: 0x0000EA58
		public float hdrEmulationScale
		{
			get
			{
				return this.m_HDREmulationScale;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00010860 File Offset: 0x0000EA60
		internal float lightRenderTextureScale
		{
			get
			{
				return this.m_LightRenderTextureScale;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00010868 File Offset: 0x0000EA68
		public Light2DBlendStyle[] lightBlendStyles
		{
			get
			{
				return this.m_LightBlendStyles;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00010870 File Offset: 0x0000EA70
		internal bool useDepthStencilBuffer
		{
			get
			{
				return this.m_UseDepthStencilBuffer;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00010878 File Offset: 0x0000EA78
		internal Texture2D fallOffLookup
		{
			get
			{
				return this.m_FallOffLookup;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00010880 File Offset: 0x0000EA80
		internal Shader shapeLightShader
		{
			get
			{
				return this.m_ShapeLightShader;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00010888 File Offset: 0x0000EA88
		internal Shader shapeLightVolumeShader
		{
			get
			{
				return this.m_ShapeLightVolumeShader;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00010890 File Offset: 0x0000EA90
		internal Shader pointLightShader
		{
			get
			{
				return this.m_PointLightShader;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00010898 File Offset: 0x0000EA98
		internal Shader pointLightVolumeShader
		{
			get
			{
				return this.m_PointLightVolumeShader;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000108A0 File Offset: 0x0000EAA0
		internal Shader blitShader
		{
			get
			{
				return this.m_BlitShader;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000108A8 File Offset: 0x0000EAA8
		internal Shader samplingShader
		{
			get
			{
				return this.m_SamplingShader;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000108B0 File Offset: 0x0000EAB0
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000108B8 File Offset: 0x0000EAB8
		internal PostProcessData postProcessData
		{
			get
			{
				return this.m_PostProcessData;
			}
			set
			{
				this.m_PostProcessData = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000108C1 File Offset: 0x0000EAC1
		internal Shader spriteShadowShader
		{
			get
			{
				return this.m_SpriteShadowShader;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000108C9 File Offset: 0x0000EAC9
		internal Shader spriteUnshadowShader
		{
			get
			{
				return this.m_SpriteUnshadowShader;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000108D1 File Offset: 0x0000EAD1
		internal Shader geometryUnshadowShader
		{
			get
			{
				return this.m_GeometryUnshadowShader;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000108D9 File Offset: 0x0000EAD9
		internal Shader projectedShadowShader
		{
			get
			{
				return this.m_ProjectedShadowShader;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000108E1 File Offset: 0x0000EAE1
		internal TransparencySortMode transparencySortMode
		{
			get
			{
				return this.m_TransparencySortMode;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000108E9 File Offset: 0x0000EAE9
		internal Vector3 transparencySortAxis
		{
			get
			{
				return this.m_TransparencySortAxis;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000108F1 File Offset: 0x0000EAF1
		internal uint lightRenderTextureMemoryBudget
		{
			get
			{
				return this.m_MaxLightRenderTextureCount;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000108F9 File Offset: 0x0000EAF9
		internal uint shadowRenderTextureMemoryBudget
		{
			get
			{
				return this.m_MaxShadowRenderTextureCount;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00010901 File Offset: 0x0000EB01
		internal bool useCameraSortingLayerTexture
		{
			get
			{
				return this.m_UseCameraSortingLayersTexture;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00010909 File Offset: 0x0000EB09
		internal int cameraSortingLayerTextureBound
		{
			get
			{
				return this.m_CameraSortingLayersTextureBound;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00010911 File Offset: 0x0000EB11
		internal Downsampling cameraSortingLayerDownsamplingMethod
		{
			get
			{
				return this.m_CameraSortingLayerDownsamplingMethod;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010919 File Offset: 0x0000EB19
		protected override ScriptableRenderer Create()
		{
			return new Renderer2D(this);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00010924 File Offset: 0x0000EB24
		protected override void OnEnable()
		{
			base.OnEnable();
			for (int i = 0; i < this.m_LightBlendStyles.Length; i++)
			{
				this.m_LightBlendStyles[i].renderTargetHandle.Init(string.Format("_ShapeLightTexture{0}", i));
			}
			this.normalsRenderTarget.Init("_NormalMap");
			this.shadowsRenderTarget.Init("_ShadowTex");
			this.spriteSelfShadowMaterial = null;
			this.spriteUnshadowMaterial = null;
			this.projectedShadowMaterial = null;
			this.stencilOnlyShadowMaterial = null;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000109AC File Offset: 0x0000EBAC
		internal Dictionary<uint, Material> lightMaterials { get; } = new Dictionary<uint, Material>();

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000109B4 File Offset: 0x0000EBB4
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000109BC File Offset: 0x0000EBBC
		internal Material[] spriteSelfShadowMaterial { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000109C5 File Offset: 0x0000EBC5
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x000109CD File Offset: 0x0000EBCD
		internal Material[] spriteUnshadowMaterial { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000109D6 File Offset: 0x0000EBD6
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x000109DE File Offset: 0x0000EBDE
		internal Material[] geometryUnshadowMaterial { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000109E7 File Offset: 0x0000EBE7
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000109EF File Offset: 0x0000EBEF
		internal Material[] projectedShadowMaterial { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000109F8 File Offset: 0x0000EBF8
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00010A00 File Offset: 0x0000EC00
		internal Material[] stencilOnlyShadowMaterial { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00010A09 File Offset: 0x0000EC09
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00010A11 File Offset: 0x0000EC11
		internal bool isNormalsRenderTargetValid { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00010A1A File Offset: 0x0000EC1A
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00010A22 File Offset: 0x0000EC22
		internal float normalsRenderTargetScale { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00010A2B File Offset: 0x0000EC2B
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00010A33 File Offset: 0x0000EC33
		internal ILight2DCullResult lightCullResult { get; set; }

		// Token: 0x04000136 RID: 310
		[SerializeField]
		private TransparencySortMode m_TransparencySortMode;

		// Token: 0x04000137 RID: 311
		[SerializeField]
		private Vector3 m_TransparencySortAxis = Vector3.up;

		// Token: 0x04000138 RID: 312
		[SerializeField]
		private float m_HDREmulationScale = 1f;

		// Token: 0x04000139 RID: 313
		[SerializeField]
		[Range(0.01f, 1f)]
		private float m_LightRenderTextureScale = 0.5f;

		// Token: 0x0400013A RID: 314
		[SerializeField]
		[FormerlySerializedAs("m_LightOperations")]
		private Light2DBlendStyle[] m_LightBlendStyles;

		// Token: 0x0400013B RID: 315
		[SerializeField]
		private bool m_UseDepthStencilBuffer = true;

		// Token: 0x0400013C RID: 316
		[SerializeField]
		private bool m_UseCameraSortingLayersTexture;

		// Token: 0x0400013D RID: 317
		[SerializeField]
		private int m_CameraSortingLayersTextureBound;

		// Token: 0x0400013E RID: 318
		[SerializeField]
		private Downsampling m_CameraSortingLayerDownsamplingMethod;

		// Token: 0x0400013F RID: 319
		[SerializeField]
		private uint m_MaxLightRenderTextureCount = 16U;

		// Token: 0x04000140 RID: 320
		[SerializeField]
		private uint m_MaxShadowRenderTextureCount = 1U;

		// Token: 0x04000141 RID: 321
		[SerializeField]
		[Reload("Shaders/2D/Light2D-Shape.shader", ReloadAttribute.Package.Root)]
		private Shader m_ShapeLightShader;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		[Reload("Shaders/2D/Light2D-Shape-Volumetric.shader", ReloadAttribute.Package.Root)]
		private Shader m_ShapeLightVolumeShader;

		// Token: 0x04000143 RID: 323
		[SerializeField]
		[Reload("Shaders/2D/Light2D-Point.shader", ReloadAttribute.Package.Root)]
		private Shader m_PointLightShader;

		// Token: 0x04000144 RID: 324
		[SerializeField]
		[Reload("Shaders/2D/Light2D-Point-Volumetric.shader", ReloadAttribute.Package.Root)]
		private Shader m_PointLightVolumeShader;

		// Token: 0x04000145 RID: 325
		[SerializeField]
		[Reload("Shaders/Utils/Blit.shader", ReloadAttribute.Package.Root)]
		private Shader m_BlitShader;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		[Reload("Shaders/Utils/Sampling.shader", ReloadAttribute.Package.Root)]
		private Shader m_SamplingShader;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		[Reload("Shaders/2D/Shadow2D-Projected.shader", ReloadAttribute.Package.Root)]
		private Shader m_ProjectedShadowShader;

		// Token: 0x04000148 RID: 328
		[SerializeField]
		[Reload("Shaders/2D/Shadow2D-Shadow-Sprite.shader", ReloadAttribute.Package.Root)]
		private Shader m_SpriteShadowShader;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		[Reload("Shaders/2D/Shadow2D-Unshadow-Sprite.shader", ReloadAttribute.Package.Root)]
		private Shader m_SpriteUnshadowShader;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		[Reload("Shaders/2D/Shadow2D-Unshadow-Geometry.shader", ReloadAttribute.Package.Root)]
		private Shader m_GeometryUnshadowShader;

		// Token: 0x0400014B RID: 331
		[SerializeField]
		[Reload("Shaders/Utils/FallbackError.shader", ReloadAttribute.Package.Root)]
		private Shader m_FallbackErrorShader;

		// Token: 0x0400014C RID: 332
		[SerializeField]
		private PostProcessData m_PostProcessData;

		// Token: 0x0400014D RID: 333
		[SerializeField]
		[Reload("Runtime/2D/Data/Textures/FalloffLookupTexture.png", ReloadAttribute.Package.Root)]
		[HideInInspector]
		private Texture2D m_FallOffLookup;

		// Token: 0x04000156 RID: 342
		internal RenderTargetHandle normalsRenderTarget;

		// Token: 0x04000157 RID: 343
		internal RenderTargetHandle shadowsRenderTarget;

		// Token: 0x04000158 RID: 344
		internal RenderTargetHandle cameraSortingLayerRenderTarget;

		// Token: 0x0200014A RID: 330
		internal enum Renderer2DDefaultMaterialType
		{
			// Token: 0x040008BF RID: 2239
			Lit,
			// Token: 0x040008C0 RID: 2240
			Unlit,
			// Token: 0x040008C1 RID: 2241
			Custom
		}
	}
}
