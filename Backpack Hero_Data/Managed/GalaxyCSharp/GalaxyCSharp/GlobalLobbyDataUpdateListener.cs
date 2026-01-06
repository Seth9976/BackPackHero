using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000087 RID: 135
	public abstract class GlobalLobbyDataUpdateListener : ILobbyDataUpdateListener
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00011C1C File Offset: 0x0000FE1C
		internal GlobalLobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00011C38 File Offset: 0x0000FE38
		public GlobalLobbyDataUpdateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00011C5A File Offset: 0x0000FE5A
		internal static HandleRef getCPtr(GlobalLobbyDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00011C78 File Offset: 0x0000FE78
		~GlobalLobbyDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A1 RID: 161
		private HandleRef swigCPtr;
	}
}
