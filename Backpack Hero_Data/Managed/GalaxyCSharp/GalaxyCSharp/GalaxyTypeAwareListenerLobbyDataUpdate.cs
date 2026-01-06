using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000039 RID: 57
	public abstract class GalaxyTypeAwareListenerLobbyDataUpdate : IGalaxyListener
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00005AA8 File Offset: 0x00003CA8
		internal GalaxyTypeAwareListenerLobbyDataUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public GalaxyTypeAwareListenerLobbyDataUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyDataUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00005AE2 File Offset: 0x00003CE2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyDataUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00005B00 File Offset: 0x00003D00
		~GalaxyTypeAwareListenerLobbyDataUpdate()
		{
			this.Dispose();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00005B30 File Offset: 0x00003D30
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyDataUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004D RID: 77
		private HandleRef swigCPtr;
	}
}
