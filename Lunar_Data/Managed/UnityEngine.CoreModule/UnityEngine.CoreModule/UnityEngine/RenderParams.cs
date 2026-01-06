using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000139 RID: 313
	public struct RenderParams
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public RenderParams(Material mat)
		{
			this.layer = 0;
			this.renderingLayerMask = GraphicsSettings.defaultRenderingLayerMask;
			this.rendererPriority = 0;
			this.worldBounds = new Bounds(Vector3.zero, Vector3.zero);
			this.camera = null;
			this.motionVectorMode = MotionVectorGenerationMode.Camera;
			this.reflectionProbeUsage = ReflectionProbeUsage.Off;
			this.material = mat;
			this.matProps = null;
			this.shadowCastingMode = ShadowCastingMode.Off;
			this.receiveShadows = false;
			this.lightProbeUsage = LightProbeUsage.Off;
			this.lightProbeProxyVolume = null;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public int layer { readonly get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		public uint renderingLayerMask { readonly get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0000EFCE File Offset: 0x0000D1CE
		public int rendererPriority { readonly get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public Bounds worldBounds { readonly get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		public Camera camera { readonly get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000EFF9 File Offset: 0x0000D1F9
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000F001 File Offset: 0x0000D201
		public MotionVectorGenerationMode motionVectorMode { readonly get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0000F00A File Offset: 0x0000D20A
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0000F012 File Offset: 0x0000D212
		public ReflectionProbeUsage reflectionProbeUsage { readonly get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000F01B File Offset: 0x0000D21B
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0000F023 File Offset: 0x0000D223
		public Material material { readonly get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000F02C File Offset: 0x0000D22C
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0000F034 File Offset: 0x0000D234
		public MaterialPropertyBlock matProps { readonly get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0000F03D File Offset: 0x0000D23D
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0000F045 File Offset: 0x0000D245
		public ShadowCastingMode shadowCastingMode { readonly get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000F04E File Offset: 0x0000D24E
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0000F056 File Offset: 0x0000D256
		public bool receiveShadows { readonly get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0000F05F File Offset: 0x0000D25F
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0000F067 File Offset: 0x0000D267
		public LightProbeUsage lightProbeUsage { readonly get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0000F070 File Offset: 0x0000D270
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0000F078 File Offset: 0x0000D278
		public LightProbeProxyVolume lightProbeProxyVolume { readonly get; set; }
	}
}
