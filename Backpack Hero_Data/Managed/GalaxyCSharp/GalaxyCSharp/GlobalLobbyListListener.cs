using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008A RID: 138
	public abstract class GlobalLobbyListListener : ILobbyListListener
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x000121B3 File Offset: 0x000103B3
		internal GlobalLobbyListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000121CF File Offset: 0x000103CF
		public GlobalLobbyListListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyList.GetListenerType(), this);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000121F1 File Offset: 0x000103F1
		internal static HandleRef getCPtr(GlobalLobbyListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00012210 File Offset: 0x00010410
		~GlobalLobbyListListener()
		{
			this.Dispose();
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00012240 File Offset: 0x00010440
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyList.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyListListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A4 RID: 164
		private HandleRef swigCPtr;
	}
}
