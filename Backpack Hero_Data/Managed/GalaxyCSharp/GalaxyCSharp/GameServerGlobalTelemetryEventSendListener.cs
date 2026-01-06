using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000068 RID: 104
	public abstract class GameServerGlobalTelemetryEventSendListener : ITelemetryEventSendListener
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		internal GameServerGlobalTelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalTelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000BDCE File Offset: 0x00009FCE
		public GameServerGlobalTelemetryEventSendListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		internal static HandleRef getCPtr(GameServerGlobalTelemetryEventSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000BE10 File Offset: 0x0000A010
		~GameServerGlobalTelemetryEventSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000BE40 File Offset: 0x0000A040
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalTelemetryEventSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007C RID: 124
		private HandleRef swigCPtr;
	}
}
