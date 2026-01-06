using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002E RID: 46
	public abstract class GalaxyTypeAwareListenerGameInvitationReceived : IGalaxyListener
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00004D6C File Offset: 0x00002F6C
		internal GalaxyTypeAwareListenerGameInvitationReceived(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGameInvitationReceived_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00004D88 File Offset: 0x00002F88
		public GalaxyTypeAwareListenerGameInvitationReceived()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerGameInvitationReceived(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00004DA6 File Offset: 0x00002FA6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerGameInvitationReceived obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00004DC4 File Offset: 0x00002FC4
		~GalaxyTypeAwareListenerGameInvitationReceived()
		{
			this.Dispose();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerGameInvitationReceived(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00004E7C File Offset: 0x0000307C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerGameInvitationReceived_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000042 RID: 66
		private HandleRef swigCPtr;
	}
}
