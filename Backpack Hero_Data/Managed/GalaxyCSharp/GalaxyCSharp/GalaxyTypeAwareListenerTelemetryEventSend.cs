using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000051 RID: 81
	public abstract class GalaxyTypeAwareListenerTelemetryEventSend : IGalaxyListener
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00007788 File Offset: 0x00005988
		internal GalaxyTypeAwareListenerTelemetryEventSend(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerTelemetryEventSend_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000077A4 File Offset: 0x000059A4
		public GalaxyTypeAwareListenerTelemetryEventSend()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerTelemetryEventSend(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000077C2 File Offset: 0x000059C2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerTelemetryEventSend obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000077E0 File Offset: 0x000059E0
		~GalaxyTypeAwareListenerTelemetryEventSend()
		{
			this.Dispose();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00007810 File Offset: 0x00005A10
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerTelemetryEventSend(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00007898 File Offset: 0x00005A98
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerTelemetryEventSend_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000065 RID: 101
		private HandleRef swigCPtr;
	}
}
