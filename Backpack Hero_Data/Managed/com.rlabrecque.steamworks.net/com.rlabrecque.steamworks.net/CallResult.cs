using System;

namespace Steamworks
{
	// Token: 0x02000179 RID: 377
	public abstract class CallResult
	{
		// Token: 0x0600089B RID: 2203
		internal abstract Type GetCallbackType();

		// Token: 0x0600089C RID: 2204
		internal abstract void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall);

		// Token: 0x0600089D RID: 2205
		internal abstract void SetUnregistered();
	}
}
