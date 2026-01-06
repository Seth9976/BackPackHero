using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008D RID: 141
	public abstract class GlobalLobbyMessageListener : ILobbyMessageListener
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00012538 File Offset: 0x00010738
		internal GlobalLobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00012554 File Offset: 0x00010754
		public GlobalLobbyMessageListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00012576 File Offset: 0x00010776
		internal static HandleRef getCPtr(GlobalLobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00012594 File Offset: 0x00010794
		~GlobalLobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000125C4 File Offset: 0x000107C4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyMessageListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A7 RID: 167
		private HandleRef swigCPtr;
	}
}
