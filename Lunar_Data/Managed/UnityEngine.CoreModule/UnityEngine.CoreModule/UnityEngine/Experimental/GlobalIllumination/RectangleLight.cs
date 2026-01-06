using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045B RID: 1115
	public struct RectangleLight
	{
		// Token: 0x04000E7D RID: 3709
		public int instanceID;

		// Token: 0x04000E7E RID: 3710
		public bool shadow;

		// Token: 0x04000E7F RID: 3711
		public LightMode mode;

		// Token: 0x04000E80 RID: 3712
		public Vector3 position;

		// Token: 0x04000E81 RID: 3713
		public Quaternion orientation;

		// Token: 0x04000E82 RID: 3714
		public LinearColor color;

		// Token: 0x04000E83 RID: 3715
		public LinearColor indirectColor;

		// Token: 0x04000E84 RID: 3716
		public float range;

		// Token: 0x04000E85 RID: 3717
		public float width;

		// Token: 0x04000E86 RID: 3718
		public float height;

		// Token: 0x04000E87 RID: 3719
		public FalloffType falloff;
	}
}
