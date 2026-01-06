using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200008E RID: 142
	public abstract class GlobalLobbyOwnerChangeListener : ILobbyOwnerChangeListener
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x000128A4 File Offset: 0x00010AA4
		internal GlobalLobbyOwnerChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyOwnerChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000128C0 File Offset: 0x00010AC0
		public GlobalLobbyOwnerChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyOwnerChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000128E2 File Offset: 0x00010AE2
		internal static HandleRef getCPtr(GlobalLobbyOwnerChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00012900 File Offset: 0x00010B00
		~GlobalLobbyOwnerChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00012930 File Offset: 0x00010B30
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyOwnerChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyOwnerChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A8 RID: 168
		private HandleRef swigCPtr;
	}
}
