using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035B RID: 859
	internal struct ImageSource
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x0007E998 File Offset: 0x0007CB98
		public bool IsNull()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x04000DAB RID: 3499
		public Texture2D texture;

		// Token: 0x04000DAC RID: 3500
		public Sprite sprite;

		// Token: 0x04000DAD RID: 3501
		public VectorImage vectorImage;

		// Token: 0x04000DAE RID: 3502
		public RenderTexture renderTexture;
	}
}
