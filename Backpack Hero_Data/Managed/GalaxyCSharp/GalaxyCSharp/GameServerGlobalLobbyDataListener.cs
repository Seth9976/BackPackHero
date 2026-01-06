using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005C RID: 92
	public abstract class GameServerGlobalLobbyDataListener : ILobbyDataListener
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x00009250 File Offset: 0x00007450
		internal GameServerGlobalLobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000926C File Offset: 0x0000746C
		public GameServerGlobalLobbyDataListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000928E File Offset: 0x0000748E
		internal static HandleRef getCPtr(GameServerGlobalLobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000092AC File Offset: 0x000074AC
		~GameServerGlobalLobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000092DC File Offset: 0x000074DC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000070 RID: 112
		private HandleRef swigCPtr;
	}
}
