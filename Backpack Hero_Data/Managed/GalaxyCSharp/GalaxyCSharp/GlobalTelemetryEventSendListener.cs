using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009F RID: 159
	public abstract class GlobalTelemetryEventSendListener : ITelemetryEventSendListener
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x000158F8 File Offset: 0x00013AF8
		internal GlobalTelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalTelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00015914 File Offset: 0x00013B14
		public GlobalTelemetryEventSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00015936 File Offset: 0x00013B36
		internal static HandleRef getCPtr(GlobalTelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00015954 File Offset: 0x00013B54
		~GlobalTelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00015984 File Offset: 0x00013B84
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalTelemetryEventSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BC RID: 188
		private HandleRef swigCPtr;
	}
}
