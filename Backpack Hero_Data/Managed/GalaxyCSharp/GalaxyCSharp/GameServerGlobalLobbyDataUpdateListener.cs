using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005E RID: 94
	public abstract class GameServerGlobalLobbyDataUpdateListener : ILobbyDataUpdateListener
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x00009A10 File Offset: 0x00007C10
		internal GameServerGlobalLobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00009A2C File Offset: 0x00007C2C
		public GameServerGlobalLobbyDataUpdateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00009A4E File Offset: 0x00007C4E
		internal static HandleRef getCPtr(GameServerGlobalLobbyDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00009A6C File Offset: 0x00007C6C
		~GameServerGlobalLobbyDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00009A9C File Offset: 0x00007C9C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000072 RID: 114
		private HandleRef swigCPtr;
	}
}
