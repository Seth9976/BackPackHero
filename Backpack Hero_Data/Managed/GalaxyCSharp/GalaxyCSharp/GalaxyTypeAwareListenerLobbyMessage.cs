using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003F RID: 63
	public abstract class GalaxyTypeAwareListenerLobbyMessage : IGalaxyListener
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x000061E0 File Offset: 0x000043E0
		internal GalaxyTypeAwareListenerLobbyMessage(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMessage_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000061FC File Offset: 0x000043FC
		public GalaxyTypeAwareListenerLobbyMessage()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMessage(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000621A File Offset: 0x0000441A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMessage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00006238 File Offset: 0x00004438
		~GalaxyTypeAwareListenerLobbyMessage()
		{
			this.Dispose();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00006268 File Offset: 0x00004468
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMessage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000062F0 File Offset: 0x000044F0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMessage_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000053 RID: 83
		private HandleRef swigCPtr;
	}
}
