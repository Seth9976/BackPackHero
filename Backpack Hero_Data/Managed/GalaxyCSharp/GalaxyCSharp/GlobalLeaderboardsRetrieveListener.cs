using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000083 RID: 131
	public abstract class GlobalLeaderboardsRetrieveListener : ILeaderboardsRetrieveListener
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0001176A File Offset: 0x0000F96A
		internal GlobalLeaderboardsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLeaderboardsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00011786 File Offset: 0x0000F986
		public GlobalLeaderboardsRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLeaderboardsRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000117A8 File Offset: 0x0000F9A8
		internal static HandleRef getCPtr(GlobalLeaderboardsRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000117C8 File Offset: 0x0000F9C8
		~GlobalLeaderboardsRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLeaderboardsRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLeaderboardsRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009D RID: 157
		private HandleRef swigCPtr;
	}
}
