using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000062 RID: 98
	public abstract class GameServerGlobalLobbyMemberStateListener : ILobbyMemberStateListener
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0000A85E File Offset: 0x00008A5E
		internal GameServerGlobalLobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000A87A File Offset: 0x00008A7A
		public GameServerGlobalLobbyMemberStateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000A89C File Offset: 0x00008A9C
		internal static HandleRef getCPtr(GameServerGlobalLobbyMemberStateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000A8BC File Offset: 0x00008ABC
		~GameServerGlobalLobbyMemberStateListener()
		{
			this.Dispose();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberState.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMemberStateListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000076 RID: 118
		private HandleRef swigCPtr;
	}
}
