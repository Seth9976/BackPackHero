using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000029 RID: 41
	internal struct SpriteSkinData
	{
		// Token: 0x04000071 RID: 113
		public NativeCustomSlice<Vector3> vertices;

		// Token: 0x04000072 RID: 114
		public NativeCustomSlice<BoneWeight> boneWeights;

		// Token: 0x04000073 RID: 115
		public NativeCustomSlice<Matrix4x4> bindPoses;

		// Token: 0x04000074 RID: 116
		public NativeCustomSlice<Vector4> tangents;

		// Token: 0x04000075 RID: 117
		public bool hasTangents;

		// Token: 0x04000076 RID: 118
		public int spriteVertexStreamSize;

		// Token: 0x04000077 RID: 119
		public int spriteVertexCount;

		// Token: 0x04000078 RID: 120
		public int tangentVertexOffset;

		// Token: 0x04000079 RID: 121
		public int deformVerticesStartPos;

		// Token: 0x0400007A RID: 122
		public int transformId;

		// Token: 0x0400007B RID: 123
		public NativeCustomSlice<int> boneTransformId;
	}
}
