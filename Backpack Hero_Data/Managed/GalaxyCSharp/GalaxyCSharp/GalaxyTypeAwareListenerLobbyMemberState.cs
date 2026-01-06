using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003E RID: 62
	public abstract class GalaxyTypeAwareListenerLobbyMemberState : IGalaxyListener
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x000060AC File Offset: 0x000042AC
		internal GalaxyTypeAwareListenerLobbyMemberState(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberState_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000060C8 File Offset: 0x000042C8
		public GalaxyTypeAwareListenerLobbyMemberState()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyMemberState(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000060E6 File Offset: 0x000042E6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyMemberState obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00006104 File Offset: 0x00004304
		~GalaxyTypeAwareListenerLobbyMemberState()
		{
			this.Dispose();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00006134 File Offset: 0x00004334
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyMemberState(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000061BC File Offset: 0x000043BC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyMemberState_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000052 RID: 82
		private HandleRef swigCPtr;
	}
}
