using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000384 RID: 900
	public interface IValueAnimation
	{
		// Token: 0x06001CD6 RID: 7382
		void Start();

		// Token: 0x06001CD7 RID: 7383
		void Stop();

		// Token: 0x06001CD8 RID: 7384
		void Recycle();

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001CD9 RID: 7385
		bool isRunning { get; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001CDA RID: 7386
		// (set) Token: 0x06001CDB RID: 7387
		int durationMs { get; set; }
	}
}
