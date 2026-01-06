using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000459 RID: 1113
	public struct PointLight
	{
		// Token: 0x04000E66 RID: 3686
		public int instanceID;

		// Token: 0x04000E67 RID: 3687
		public bool shadow;

		// Token: 0x04000E68 RID: 3688
		public LightMode mode;

		// Token: 0x04000E69 RID: 3689
		public Vector3 position;

		// Token: 0x04000E6A RID: 3690
		public Quaternion orientation;

		// Token: 0x04000E6B RID: 3691
		public LinearColor color;

		// Token: 0x04000E6C RID: 3692
		public LinearColor indirectColor;

		// Token: 0x04000E6D RID: 3693
		public float range;

		// Token: 0x04000E6E RID: 3694
		public float sphereRadius;

		// Token: 0x04000E6F RID: 3695
		public FalloffType falloff;
	}
}
