using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013B RID: 315
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	internal struct Internal_DrawTextureArguments
	{
		// Token: 0x040003FD RID: 1021
		public Rect screenRect;

		// Token: 0x040003FE RID: 1022
		public Rect sourceRect;

		// Token: 0x040003FF RID: 1023
		public int leftBorder;

		// Token: 0x04000400 RID: 1024
		public int rightBorder;

		// Token: 0x04000401 RID: 1025
		public int topBorder;

		// Token: 0x04000402 RID: 1026
		public int bottomBorder;

		// Token: 0x04000403 RID: 1027
		public Color leftBorderColor;

		// Token: 0x04000404 RID: 1028
		public Color rightBorderColor;

		// Token: 0x04000405 RID: 1029
		public Color topBorderColor;

		// Token: 0x04000406 RID: 1030
		public Color bottomBorderColor;

		// Token: 0x04000407 RID: 1031
		public Color color;

		// Token: 0x04000408 RID: 1032
		public Vector4 borderWidths;

		// Token: 0x04000409 RID: 1033
		public Vector4 cornerRadiuses;

		// Token: 0x0400040A RID: 1034
		public bool smoothCorners;

		// Token: 0x0400040B RID: 1035
		public int pass;

		// Token: 0x0400040C RID: 1036
		public Texture texture;

		// Token: 0x0400040D RID: 1037
		public Material mat;
	}
}
