using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000141 RID: 321
	internal interface ISavedState
	{
		// Token: 0x06001183 RID: 4483
		void StaticDisposeCurrentState();

		// Token: 0x06001184 RID: 4484
		void RestoreSavedState();
	}
}
