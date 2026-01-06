using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000243 RID: 579
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x02000244 RID: 580
		public struct MeshInstance
		{
			// Token: 0x04000850 RID: 2128
			public int meshInstanceID;

			// Token: 0x04000851 RID: 2129
			public int rendererInstanceID;

			// Token: 0x04000852 RID: 2130
			public int additionalVertexStreamsMeshInstanceID;

			// Token: 0x04000853 RID: 2131
			public int enlightenVertexStreamMeshInstanceID;

			// Token: 0x04000854 RID: 2132
			public Matrix4x4 transform;

			// Token: 0x04000855 RID: 2133
			public Vector4 lightmapScaleOffset;

			// Token: 0x04000856 RID: 2134
			public Vector4 realtimeLightmapScaleOffset;
		}

		// Token: 0x02000245 RID: 581
		public struct SubMeshInstance
		{
			// Token: 0x04000857 RID: 2135
			public int meshInstanceID;

			// Token: 0x04000858 RID: 2136
			public int vertexOffset;

			// Token: 0x04000859 RID: 2137
			public int gameObjectInstanceID;

			// Token: 0x0400085A RID: 2138
			public int subMeshIndex;

			// Token: 0x0400085B RID: 2139
			public Matrix4x4 transform;
		}

		// Token: 0x02000246 RID: 582
		public struct MeshContainer
		{
			// Token: 0x0400085C RID: 2140
			public GameObject gameObject;

			// Token: 0x0400085D RID: 2141
			public MeshSubsetCombineUtility.MeshInstance instance;

			// Token: 0x0400085E RID: 2142
			public List<MeshSubsetCombineUtility.SubMeshInstance> subMeshInstances;
		}
	}
}
