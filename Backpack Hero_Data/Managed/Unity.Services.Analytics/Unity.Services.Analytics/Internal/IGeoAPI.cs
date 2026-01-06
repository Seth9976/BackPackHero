using System;
using System.Threading.Tasks;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200002E RID: 46
	internal interface IGeoAPI
	{
		// Token: 0x060000FE RID: 254
		Task<GeoIPResponse> MakeRequest();
	}
}
