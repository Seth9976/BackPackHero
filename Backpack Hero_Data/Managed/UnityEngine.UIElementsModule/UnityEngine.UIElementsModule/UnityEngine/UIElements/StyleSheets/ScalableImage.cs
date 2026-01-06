using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000359 RID: 857
	[Serializable]
	internal struct ScalableImage
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x0007E954 File Offset: 0x0007CB54
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "normalImage", this.normalImage, "highResolutionImage", this.highResolutionImage });
		}

		// Token: 0x04000DA7 RID: 3495
		public Texture2D normalImage;

		// Token: 0x04000DA8 RID: 3496
		public Texture2D highResolutionImage;
	}
}
