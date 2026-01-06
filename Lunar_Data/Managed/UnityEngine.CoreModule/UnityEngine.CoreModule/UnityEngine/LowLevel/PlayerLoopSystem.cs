using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x020002EB RID: 747
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public struct PlayerLoopSystem
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x000315B2 File Offset: 0x0002F7B2
		public override string ToString()
		{
			return this.type.Name;
		}

		// Token: 0x040009F6 RID: 2550
		public Type type;

		// Token: 0x040009F7 RID: 2551
		public PlayerLoopSystem[] subSystemList;

		// Token: 0x040009F8 RID: 2552
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x040009F9 RID: 2553
		public IntPtr updateFunction;

		// Token: 0x040009FA RID: 2554
		public IntPtr loopConditionFunction;

		// Token: 0x020002EC RID: 748
		// (Invoke) Token: 0x06001E77 RID: 7799
		public delegate void UpdateFunction();
	}
}
