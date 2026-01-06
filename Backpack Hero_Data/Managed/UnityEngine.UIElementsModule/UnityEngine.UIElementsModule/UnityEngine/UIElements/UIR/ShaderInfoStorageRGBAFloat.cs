using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000318 RID: 792
	internal class ShaderInfoStorageRGBAFloat : ShaderInfoStorage<Color>
	{
		// Token: 0x060019B5 RID: 6581 RVA: 0x0006AA78 File Offset: 0x00068C78
		public ShaderInfoStorageRGBAFloat(int initialSize = 64, int maxSize = 4096)
			: base(TextureFormat.RGBAFloat, ShaderInfoStorageRGBAFloat.s_Convert, initialSize, maxSize)
		{
		}

		// Token: 0x04000B9E RID: 2974
		private static readonly Func<Color, Color> s_Convert = (Color c) => c;
	}
}
