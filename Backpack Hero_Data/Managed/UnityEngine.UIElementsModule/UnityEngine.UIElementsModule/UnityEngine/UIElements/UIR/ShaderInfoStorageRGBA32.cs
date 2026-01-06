using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000316 RID: 790
	internal class ShaderInfoStorageRGBA32 : ShaderInfoStorage<Color32>
	{
		// Token: 0x060019B0 RID: 6576 RVA: 0x0006AA3B File Offset: 0x00068C3B
		public ShaderInfoStorageRGBA32(int initialSize = 64, int maxSize = 4096)
			: base(TextureFormat.RGBA32, ShaderInfoStorageRGBA32.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x04000B9C RID: 2972
		private static readonly Func<Color, Color32> s_Convert = (Color c) => c;
	}
}
