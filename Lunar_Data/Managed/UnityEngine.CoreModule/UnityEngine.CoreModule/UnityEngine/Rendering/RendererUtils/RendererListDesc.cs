using System;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x02000429 RID: 1065
	public struct RendererListDesc
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x0003EA13 File Offset: 0x0003CC13
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x0003EA1B File Offset: 0x0003CC1B
		internal CullingResults cullingResult { readonly get; private set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x0003EA24 File Offset: 0x0003CC24
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x0003EA2C File Offset: 0x0003CC2C
		internal Camera camera { readonly get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x0003EA35 File Offset: 0x0003CC35
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x0003EA3D File Offset: 0x0003CC3D
		internal ShaderTagId passName { readonly get; private set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0003EA46 File Offset: 0x0003CC46
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x0003EA4E File Offset: 0x0003CC4E
		internal ShaderTagId[] passNames { readonly get; private set; }

		// Token: 0x06002525 RID: 9509 RVA: 0x0003EA57 File Offset: 0x0003CC57
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

		// Token: 0x06002526 RID: 9510 RVA: 0x0003EA8F File Offset: 0x0003CC8F
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

		// Token: 0x06002527 RID: 9511 RVA: 0x0003EACC File Offset: 0x0003CCCC
		public bool IsValid()
		{
			bool flag = this.camera == null || (this.passName == ShaderTagId.none && (this.passNames == null || this.passNames.Length == 0));
			return !flag;
		}

		// Token: 0x04000DD0 RID: 3536
		public SortingCriteria sortingCriteria;

		// Token: 0x04000DD1 RID: 3537
		public PerObjectData rendererConfiguration;

		// Token: 0x04000DD2 RID: 3538
		public RenderQueueRange renderQueueRange;

		// Token: 0x04000DD3 RID: 3539
		public RenderStateBlock? stateBlock;

		// Token: 0x04000DD4 RID: 3540
		public Material overrideMaterial;

		// Token: 0x04000DD5 RID: 3541
		public bool excludeObjectMotionVectors;

		// Token: 0x04000DD6 RID: 3542
		public int layerMask;

		// Token: 0x04000DD7 RID: 3543
		public int overrideMaterialPassIndex;
	}
}
