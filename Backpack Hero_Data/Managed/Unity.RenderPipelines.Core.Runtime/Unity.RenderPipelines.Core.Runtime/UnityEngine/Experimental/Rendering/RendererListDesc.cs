using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200001A RID: 26
	[Obsolete("Use the updated RendererList API which is defined in the UnityEngine.Rendering.RendererUtils namespace.")]
	public struct RendererListDesc
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006F1A File Offset: 0x0000511A
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006F22 File Offset: 0x00005122
		internal CullingResults cullingResult { readonly get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006F2B File Offset: 0x0000512B
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006F33 File Offset: 0x00005133
		internal Camera camera { readonly get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006F3C File Offset: 0x0000513C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006F44 File Offset: 0x00005144
		internal ShaderTagId passName { readonly get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006F4D File Offset: 0x0000514D
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006F55 File Offset: 0x00005155
		internal ShaderTagId[] passNames { readonly get; private set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00006F5E File Offset: 0x0000515E
		public RendererListDesc(ShaderTagId passName, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passName = passName;
			this.passNames = null;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006F91 File Offset: 0x00005191
		public RendererListDesc(ShaderTagId[] passNames, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passNames = passNames;
			this.passName = ShaderTagId.none;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006FC8 File Offset: 0x000051C8
		public bool IsValid()
		{
			return !(this.camera == null) && (!(this.passName == ShaderTagId.none) || (this.passNames != null && this.passNames.Length != 0));
		}

		// Token: 0x040000B6 RID: 182
		public SortingCriteria sortingCriteria;

		// Token: 0x040000B7 RID: 183
		public PerObjectData rendererConfiguration;

		// Token: 0x040000B8 RID: 184
		public RenderQueueRange renderQueueRange;

		// Token: 0x040000B9 RID: 185
		public RenderStateBlock? stateBlock;

		// Token: 0x040000BA RID: 186
		public Material overrideMaterial;

		// Token: 0x040000BB RID: 187
		public bool excludeObjectMotionVectors;

		// Token: 0x040000BC RID: 188
		public int layerMask;

		// Token: 0x040000BD RID: 189
		public int overrideMaterialPassIndex;
	}
}
