using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000034 RID: 52
	public interface ITimeControl
	{
		// Token: 0x06000284 RID: 644
		void SetTime(double time);

		// Token: 0x06000285 RID: 645
		void OnControlTimeStart();

		// Token: 0x06000286 RID: 646
		void OnControlTimeStop();
	}
}
