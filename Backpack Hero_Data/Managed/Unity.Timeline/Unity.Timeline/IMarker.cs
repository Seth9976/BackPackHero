using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000024 RID: 36
	public interface IMarker
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000223 RID: 547
		// (set) Token: 0x06000224 RID: 548
		double time { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000225 RID: 549
		TrackAsset parent { get; }

		// Token: 0x06000226 RID: 550
		void Initialize(TrackAsset parent);
	}
}
