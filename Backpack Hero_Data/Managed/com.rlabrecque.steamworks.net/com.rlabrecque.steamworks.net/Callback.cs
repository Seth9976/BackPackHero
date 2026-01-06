using System;

namespace Steamworks
{
	// Token: 0x02000177 RID: 375
	public abstract class Callback
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000889 RID: 2185
		public abstract bool IsGameServer { get; }

		// Token: 0x0600088A RID: 2186
		internal abstract Type GetCallbackType();

		// Token: 0x0600088B RID: 2187
		internal abstract void OnRunCallback(IntPtr pvParam);

		// Token: 0x0600088C RID: 2188
		internal abstract void SetUnregistered();
	}
}
