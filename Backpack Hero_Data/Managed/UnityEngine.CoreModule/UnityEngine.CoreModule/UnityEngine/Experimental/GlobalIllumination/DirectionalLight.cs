using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000458 RID: 1112
	public struct DirectionalLight
	{
		// Token: 0x04000E5D RID: 3677
		public int instanceID;

		// Token: 0x04000E5E RID: 3678
		public bool shadow;

		// Token: 0x04000E5F RID: 3679
		public LightMode mode;

		// Token: 0x04000E60 RID: 3680
		public Vector3 position;

		// Token: 0x04000E61 RID: 3681
		public Quaternion orientation;

		// Token: 0x04000E62 RID: 3682
		public LinearColor color;

		// Token: 0x04000E63 RID: 3683
		public LinearColor indirectColor;

		// Token: 0x04000E64 RID: 3684
		public float penumbraWidthRadian;

		// Token: 0x04000E65 RID: 3685
		[Obsolete("Directional lights support cookies now. In order to position the cookie projection in the world, a position and full orientation are necessary. Use the position and orientation members instead of the direction parameter.", true)]
		public Vector3 direction;
	}
}
