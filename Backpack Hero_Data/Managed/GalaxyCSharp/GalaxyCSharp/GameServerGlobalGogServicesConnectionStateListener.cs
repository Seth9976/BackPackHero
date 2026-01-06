using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005A RID: 90
	public abstract class GameServerGlobalGogServicesConnectionStateListener : IGogServicesConnectionStateListener
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00008B91 File Offset: 0x00006D91
		internal GameServerGlobalGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00008BAD File Offset: 0x00006DAD
		public GameServerGlobalGogServicesConnectionStateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00008BCF File Offset: 0x00006DCF
		internal static HandleRef getCPtr(GameServerGlobalGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00008BF0 File Offset: 0x00006DF0
		~GameServerGlobalGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00008C20 File Offset: 0x00006E20
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalGogServicesConnectionStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006E RID: 110
		private HandleRef swigCPtr;
	}
}
