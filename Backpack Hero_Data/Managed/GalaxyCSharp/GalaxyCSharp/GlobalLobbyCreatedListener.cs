using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000084 RID: 132
	public abstract class GlobalLobbyCreatedListener : ILobbyCreatedListener
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x00011898 File Offset: 0x0000FA98
		internal GlobalLobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000118B4 File Offset: 0x0000FAB4
		public GlobalLobbyCreatedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000118D6 File Offset: 0x0000FAD6
		internal static HandleRef getCPtr(GlobalLobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000118F4 File Offset: 0x0000FAF4
		~GlobalLobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00011924 File Offset: 0x0000FB24
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyCreatedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009E RID: 158
		private HandleRef swigCPtr;
	}
}
