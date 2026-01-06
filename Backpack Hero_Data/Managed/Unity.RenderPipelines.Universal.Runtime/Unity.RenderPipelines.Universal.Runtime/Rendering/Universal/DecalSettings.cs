using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	internal class DecalSettings
	{
		// Token: 0x04000410 RID: 1040
		public DecalTechniqueOption technique;

		// Token: 0x04000411 RID: 1041
		public float maxDrawDistance = 1000f;

		// Token: 0x04000412 RID: 1042
		public DBufferSettings dBufferSettings;

		// Token: 0x04000413 RID: 1043
		public DecalScreenSpaceSettings screenSpaceSettings;
	}
}
