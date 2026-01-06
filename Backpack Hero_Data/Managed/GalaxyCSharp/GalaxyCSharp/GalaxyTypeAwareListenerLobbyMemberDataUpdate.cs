using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003D RID: 61
	public abstract class GalaxyTypeAwareListenerLobbyMemberDataUpdate : IGalaxyListener
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00005F78 File Offset: 0x00004178
		internal GalaxyTypeAwareListenerLobbyMemberDataUpdate(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberDataUpdate_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00005F94 File Offset: 0x00004194
		public GalaxyTypeAwareListenerLobbyMemberDataUpdate()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMemberDataUpdate(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00005FB2 File Offset: 0x000041B2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMemberDataUpdate obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00005FD0 File Offset: 0x000041D0
		~GalaxyTypeAwareListenerLobbyMemberDataUpdate()
		{
			this.Dispose();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00006000 File Offset: 0x00004200
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMemberDataUpdate(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00006088 File Offset: 0x00004288
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberDataUpdate_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000051 RID: 81
		private HandleRef swigCPtr;
	}
}
