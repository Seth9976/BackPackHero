using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x020002EA RID: 746
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	[RequiredByNativeCode]
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	internal struct PlayerLoopSystemInternal
	{
		// Token: 0x040009F1 RID: 2545
		public Type type;

		// Token: 0x040009F2 RID: 2546
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x040009F3 RID: 2547
		public IntPtr updateFunction;

		// Token: 0x040009F4 RID: 2548
		public IntPtr loopConditionFunction;

		// Token: 0x040009F5 RID: 2549
		public int numSubSystems;
	}
}
