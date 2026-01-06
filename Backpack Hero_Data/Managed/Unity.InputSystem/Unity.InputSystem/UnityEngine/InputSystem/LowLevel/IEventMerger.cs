using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C5 RID: 197
	internal interface IEventMerger
	{
		// Token: 0x06000CD1 RID: 3281
		bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr);
	}
}
