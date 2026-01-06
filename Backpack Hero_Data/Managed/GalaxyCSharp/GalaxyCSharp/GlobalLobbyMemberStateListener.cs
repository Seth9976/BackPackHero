using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008C RID: 140
	public abstract class GlobalLobbyMemberStateListener : ILobbyMemberStateListener
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0001240C File Offset: 0x0001060C
		internal GlobalLobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00012428 File Offset: 0x00010628
		public GlobalLobbyMemberStateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001244A File Offset: 0x0001064A
		internal static HandleRef getCPtr(GlobalLobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00012468 File Offset: 0x00010668
		~GlobalLobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00012498 File Offset: 0x00010698
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyMemberStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A6 RID: 166
		private HandleRef swigCPtr;
	}
}
