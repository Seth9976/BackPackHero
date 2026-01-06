using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000225 RID: 549
	public interface ITransitionEvent
	{
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600107C RID: 4220
		StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600107D RID: 4221
		double elapsedTime { get; }
	}
}
