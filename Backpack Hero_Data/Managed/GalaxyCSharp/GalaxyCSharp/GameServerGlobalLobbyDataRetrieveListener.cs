using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005D RID: 93
	public abstract class GameServerGlobalLobbyDataRetrieveListener : ILobbyDataRetrieveListener
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00009630 File Offset: 0x00007830
		internal GameServerGlobalLobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000964C File Offset: 0x0000784C
		public GameServerGlobalLobbyDataRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000966E File Offset: 0x0000786E
		internal static HandleRef getCPtr(GameServerGlobalLobbyDataRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000968C File Offset: 0x0000788C
		~GameServerGlobalLobbyDataRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000096BC File Offset: 0x000078BC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyDataRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000071 RID: 113
		private HandleRef swigCPtr;
	}
}
