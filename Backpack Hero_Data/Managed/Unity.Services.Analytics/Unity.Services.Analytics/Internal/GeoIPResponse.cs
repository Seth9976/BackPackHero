using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal class GeoIPResponse
	{
		// Token: 0x040000C7 RID: 199
		public string identifier;

		// Token: 0x040000C8 RID: 200
		public string country;

		// Token: 0x040000C9 RID: 201
		public string region;

		// Token: 0x040000CA RID: 202
		public int ageGateLimit;
	}
}
