using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BD RID: 957
	public struct SubMeshDescriptor
	{
		// Token: 0x06001F49 RID: 8009 RVA: 0x00032F58 File Offset: 0x00031158
		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00032FA6 File Offset: 0x000311A6
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x00032FAE File Offset: 0x000311AE
		public Bounds bounds { readonly get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00032FB7 File Offset: 0x000311B7
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x00032FBF File Offset: 0x000311BF
		public MeshTopology topology { readonly get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x00032FC8 File Offset: 0x000311C8
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x00032FD0 File Offset: 0x000311D0
		public int indexStart { readonly get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00032FD9 File Offset: 0x000311D9
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00032FE1 File Offset: 0x000311E1
		public int indexCount { readonly get; set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00032FEA File Offset: 0x000311EA
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x00032FF2 File Offset: 0x000311F2
		public int baseVertex { readonly get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x00032FFB File Offset: 0x000311FB
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x00033003 File Offset: 0x00031203
		public int firstVertex { readonly get; set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0003300C File Offset: 0x0003120C
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x00033014 File Offset: 0x00031214
		public int vertexCount { readonly get; set; }

		// Token: 0x06001F58 RID: 8024 RVA: 0x00033020 File Offset: 0x00031220
		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[] { this.topology, this.indexStart, this.indexCount, this.firstVertex, this.vertexCount, this.baseVertex, this.bounds });
		}
	}
}
