using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A0 RID: 928
	[Flags]
	public enum MeshUpdateFlags
	{
		// Token: 0x04000A3F RID: 2623
		Default = 0,
		// Token: 0x04000A40 RID: 2624
		DontValidateIndices = 1,
		// Token: 0x04000A41 RID: 2625
		DontResetBoneBounds = 2,
		// Token: 0x04000A42 RID: 2626
		DontNotifyMeshUsers = 4,
		// Token: 0x04000A43 RID: 2627
		DontRecalculateBounds = 8
	}
}
