using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007F RID: 127
	public abstract class GlobalGogServicesConnectionStateListener : IGogServicesConnectionStateListener
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x0001084C File Offset: 0x0000EA4C
		internal GlobalGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00010868 File Offset: 0x0000EA68
		public GlobalGogServicesConnectionStateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001088A File Offset: 0x0000EA8A
		internal static HandleRef getCPtr(GlobalGogServicesConnectionStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000108A8 File Offset: 0x0000EAA8
		~GlobalGogServicesConnectionStateListener()
		{
			this.Dispose();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGogServicesConnectionStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000099 RID: 153
		private HandleRef swigCPtr;
	}
}
