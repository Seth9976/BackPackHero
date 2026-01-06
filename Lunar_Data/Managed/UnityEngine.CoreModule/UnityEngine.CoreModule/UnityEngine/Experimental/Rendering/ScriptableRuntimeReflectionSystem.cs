using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200046E RID: 1134
	public abstract class ScriptableRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x06002817 RID: 10263 RVA: 0x00042AC8 File Offset: 0x00040CC8
		public virtual bool TickRealtimeProbes()
		{
			return false;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00004557 File Offset: 0x00002757
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00042ADB File Offset: 0x00040CDB
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
