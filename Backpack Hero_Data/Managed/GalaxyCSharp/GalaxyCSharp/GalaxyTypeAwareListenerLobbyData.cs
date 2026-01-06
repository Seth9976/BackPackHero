using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000037 RID: 55
	public abstract class GalaxyTypeAwareListenerLobbyData : IGalaxyListener
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00005840 File Offset: 0x00003A40
		internal GalaxyTypeAwareListenerLobbyData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000585C File Offset: 0x00003A5C
		public GalaxyTypeAwareListenerLobbyData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000587A File Offset: 0x00003A7A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00005898 File Offset: 0x00003A98
		~GalaxyTypeAwareListenerLobbyData()
		{
			this.Dispose();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000058C8 File Offset: 0x00003AC8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00005950 File Offset: 0x00003B50
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004B RID: 75
		private HandleRef swigCPtr;
	}
}
