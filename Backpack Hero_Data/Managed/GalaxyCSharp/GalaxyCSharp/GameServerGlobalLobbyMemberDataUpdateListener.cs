using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000061 RID: 97
	public abstract class GameServerGlobalLobbyMemberDataUpdateListener : ILobbyMemberDataUpdateListener
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0000A4E2 File Offset: 0x000086E2
		internal GameServerGlobalLobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000A4FE File Offset: 0x000086FE
		public GameServerGlobalLobbyMemberDataUpdateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000A520 File Offset: 0x00008720
		internal static HandleRef getCPtr(GameServerGlobalLobbyMemberDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000A540 File Offset: 0x00008740
		~GameServerGlobalLobbyMemberDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000A570 File Offset: 0x00008770
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMemberDataUpdateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000075 RID: 117
		private HandleRef swigCPtr;
	}
}
