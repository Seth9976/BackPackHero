using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public struct XRFeatureDescriptor
	{
		// Token: 0x0400033A RID: 826
		public string name;

		// Token: 0x0400033B RID: 827
		public List<UsageHint> usageHints;

		// Token: 0x0400033C RID: 828
		public FeatureType featureType;

		// Token: 0x0400033D RID: 829
		public uint customSize;
	}
}
