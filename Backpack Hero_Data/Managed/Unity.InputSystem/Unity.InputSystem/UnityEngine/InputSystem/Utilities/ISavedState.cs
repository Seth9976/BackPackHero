using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000141 RID: 321
	internal interface ISavedState
	{
		// Token: 0x0600118A RID: 4490
		void StaticDisposeCurrentState();

		// Token: 0x0600118B RID: 4491
		void RestoreSavedState();
	}
}
