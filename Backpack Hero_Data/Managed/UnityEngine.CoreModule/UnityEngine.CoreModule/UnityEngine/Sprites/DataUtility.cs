using System;

namespace UnityEngine.Sprites
{
	// Token: 0x0200026C RID: 620
	public sealed class DataUtility
	{
		// Token: 0x06001B0B RID: 6923 RVA: 0x0002B57C File Offset: 0x0002977C
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			return sprite.GetInnerUVs();
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0002B594 File Offset: 0x00029794
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			return sprite.GetOuterUVs();
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0002B5AC File Offset: 0x000297AC
		public static Vector4 GetPadding(Sprite sprite)
		{
			return sprite.GetPadding();
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0002B5C4 File Offset: 0x000297C4
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 vector;
			vector.x = sprite.border.x + sprite.border.z;
			vector.y = sprite.border.y + sprite.border.w;
			return vector;
		}
	}
}
