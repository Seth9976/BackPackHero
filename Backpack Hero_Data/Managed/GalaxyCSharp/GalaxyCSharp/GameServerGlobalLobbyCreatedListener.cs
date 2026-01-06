using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005B RID: 91
	public abstract class GameServerGlobalLobbyCreatedListener : ILobbyCreatedListener
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x00008EE3 File Offset: 0x000070E3
		internal GameServerGlobalLobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00008EFF File Offset: 0x000070FF
		public GameServerGlobalLobbyCreatedListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00008F21 File Offset: 0x00007121
		internal static HandleRef getCPtr(GameServerGlobalLobbyCreatedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00008F40 File Offset: 0x00007140
		~GameServerGlobalLobbyCreatedListener()
		{
			this.Dispose();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00008F70 File Offset: 0x00007170
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyCreated.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyCreatedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006F RID: 111
		private HandleRef swigCPtr;
	}
}
