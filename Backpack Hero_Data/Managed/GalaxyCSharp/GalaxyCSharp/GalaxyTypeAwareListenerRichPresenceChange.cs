using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004A RID: 74
	public abstract class GalaxyTypeAwareListenerRichPresenceChange : IGalaxyListener
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00006F1C File Offset: 0x0000511C
		internal GalaxyTypeAwareListenerRichPresenceChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00006F38 File Offset: 0x00005138
		public GalaxyTypeAwareListenerRichPresenceChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresenceChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00006F56 File Offset: 0x00005156
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresenceChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00006F74 File Offset: 0x00005174
		~GalaxyTypeAwareListenerRichPresenceChange()
		{
			this.Dispose();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00006FA4 File Offset: 0x000051A4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresenceChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000702C File Offset: 0x0000522C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresenceChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005E RID: 94
		private HandleRef swigCPtr;
	}
}
