using System;

namespace TwitchLib.Api.Core.RateLimiter
{
	// Token: 0x02000008 RID: 8
	public class DisposeAction : IDisposable
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002E66 File Offset: 0x00001066
		public DisposeAction(Action act)
		{
			this._act = act;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E75 File Offset: 0x00001075
		public void Dispose()
		{
			Action act = this._act;
			if (act != null)
			{
				act.Invoke();
			}
			this._act = null;
		}

		// Token: 0x04000019 RID: 25
		private Action _act;
	}
}
