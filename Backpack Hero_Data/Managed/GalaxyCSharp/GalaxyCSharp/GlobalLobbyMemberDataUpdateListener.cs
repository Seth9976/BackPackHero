using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008B RID: 139
	public abstract class GlobalLobbyMemberDataUpdateListener : ILobbyMemberDataUpdateListener
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x000122E0 File Offset: 0x000104E0
		internal GlobalLobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000122FC File Offset: 0x000104FC
		public GlobalLobbyMemberDataUpdateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001231E File Offset: 0x0001051E
		internal static HandleRef getCPtr(GlobalLobbyMemberDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001233C File Offset: 0x0001053C
		~GlobalLobbyMemberDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001236C File Offset: 0x0001056C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyMemberDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A5 RID: 165
		private HandleRef swigCPtr;
	}
}
