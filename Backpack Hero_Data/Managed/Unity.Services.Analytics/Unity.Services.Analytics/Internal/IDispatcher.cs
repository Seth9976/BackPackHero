using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000035 RID: 53
	internal interface IDispatcher
	{
		// Token: 0x06000116 RID: 278
		void SetBuffer(IBuffer buffer);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000117 RID: 279
		// (set) Token: 0x06000118 RID: 280
		string CollectUrl { get; set; }

		// Token: 0x06000119 RID: 281
		void Flush();
	}
}
