using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000086 RID: 134
	public abstract class GlobalLobbyDataRetrieveListener : ILobbyDataRetrieveListener
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x00011AF0 File Offset: 0x0000FCF0
		internal GlobalLobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public GlobalLobbyDataRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00011B2E File Offset: 0x0000FD2E
		internal static HandleRef getCPtr(GlobalLobbyDataRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00011B4C File Offset: 0x0000FD4C
		~GlobalLobbyDataRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A0 RID: 160
		private HandleRef swigCPtr;
	}
}
