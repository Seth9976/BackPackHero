using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x02000451 RID: 1105
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	internal struct SpriteIntermediateRendererInfo
	{
		// Token: 0x04000E34 RID: 3636
		public int SpriteID;

		// Token: 0x04000E35 RID: 3637
		public int TextureID;

		// Token: 0x04000E36 RID: 3638
		public int MaterialID;

		// Token: 0x04000E37 RID: 3639
		public Color Color;

		// Token: 0x04000E38 RID: 3640
		public Matrix4x4 Transform;

		// Token: 0x04000E39 RID: 3641
		public Bounds Bounds;

		// Token: 0x04000E3A RID: 3642
		public int Layer;

		// Token: 0x04000E3B RID: 3643
		public int SortingLayer;

		// Token: 0x04000E3C RID: 3644
		public int SortingOrder;

		// Token: 0x04000E3D RID: 3645
		public ulong SceneCullingMask;

		// Token: 0x04000E3E RID: 3646
		public IntPtr IndexData;

		// Token: 0x04000E3F RID: 3647
		public IntPtr VertexData;

		// Token: 0x04000E40 RID: 3648
		public int IndexCount;

		// Token: 0x04000E41 RID: 3649
		public int VertexCount;

		// Token: 0x04000E42 RID: 3650
		public int ShaderChannelMask;
	}
}
