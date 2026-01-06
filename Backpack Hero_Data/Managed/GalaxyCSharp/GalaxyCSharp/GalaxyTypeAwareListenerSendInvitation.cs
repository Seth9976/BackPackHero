using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004C RID: 76
	public abstract class GalaxyTypeAwareListenerSendInvitation : IGalaxyListener
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00007184 File Offset: 0x00005384
		internal GalaxyTypeAwareListenerSendInvitation(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSendInvitation_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000071A0 File Offset: 0x000053A0
		public GalaxyTypeAwareListenerSendInvitation()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSendInvitation(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000071BE File Offset: 0x000053BE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSendInvitation obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000071DC File Offset: 0x000053DC
		~GalaxyTypeAwareListenerSendInvitation()
		{
			this.Dispose();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000720C File Offset: 0x0000540C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSendInvitation(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00007294 File Offset: 0x00005494
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSendInvitation_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000060 RID: 96
		private HandleRef swigCPtr;
	}
}
