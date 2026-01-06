using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000082 RID: 130
	public abstract class GlobalLeaderboardScoreUpdateListener : ILeaderboardScoreUpdateListener
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x000113D4 File Offset: 0x0000F5D4
		internal GlobalLeaderboardScoreUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardScoreUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public GlobalLeaderboardScoreUpdateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardScoreUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00011412 File Offset: 0x0000F612
		internal static HandleRef getCPtr(GlobalLeaderboardScoreUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00011430 File Offset: 0x0000F630
		~GlobalLeaderboardScoreUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00011460 File Offset: 0x0000F660
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardScoreUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardScoreUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009C RID: 156
		private HandleRef swigCPtr;
	}
}
