using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045F RID: 1119
	public struct Cookie
	{
		// Token: 0x060027B3 RID: 10163 RVA: 0x00041720 File Offset: 0x0003F920
		public static Cookie Defaults()
		{
			Cookie cookie;
			cookie.instanceID = 0;
			cookie.scale = 1f;
			cookie.sizes = new Vector2(1f, 1f);
			return cookie;
		}

		// Token: 0x04000EA7 RID: 3751
		public int instanceID;

		// Token: 0x04000EA8 RID: 3752
		public float scale;

		// Token: 0x04000EA9 RID: 3753
		public Vector2 sizes;
	}
}
