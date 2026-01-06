using System;

namespace UnityEngine
{
	// Token: 0x020001A0 RID: 416
	public struct CombineInstance
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00015FA0 File Offset: 0x000141A0
		// (set) Token: 0x060010AD RID: 4269 RVA: 0x00015FBD File Offset: 0x000141BD
		public Mesh mesh
		{
			get
			{
				return Mesh.FromInstanceID(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x00015FD8 File Offset: 0x000141D8
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x00015FF0 File Offset: 0x000141F0
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00015FFC File Offset: 0x000141FC
		// (set) Token: 0x060010B1 RID: 4273 RVA: 0x00016014 File Offset: 0x00014214
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00016020 File Offset: 0x00014220
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x00016038 File Offset: 0x00014238
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.m_LightmapScaleOffset;
			}
			set
			{
				this.m_LightmapScaleOffset = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00016044 File Offset: 0x00014244
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x0001605C File Offset: 0x0001425C
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.m_RealtimeLightmapScaleOffset;
			}
			set
			{
				this.m_RealtimeLightmapScaleOffset = value;
			}
		}

		// Token: 0x040005B1 RID: 1457
		private int m_MeshInstanceID;

		// Token: 0x040005B2 RID: 1458
		private int m_SubMeshIndex;

		// Token: 0x040005B3 RID: 1459
		private Matrix4x4 m_Transform;

		// Token: 0x040005B4 RID: 1460
		private Vector4 m_LightmapScaleOffset;

		// Token: 0x040005B5 RID: 1461
		private Vector4 m_RealtimeLightmapScaleOffset;
	}
}
