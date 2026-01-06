using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200005F RID: 95
	public abstract class GameServerGlobalLobbyEnteredListener : ILobbyEnteredListener
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x00009D5F File Offset: 0x00007F5F
		internal GameServerGlobalLobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00009D7B File Offset: 0x00007F7B
		public GameServerGlobalLobbyEnteredListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00009D9D File Offset: 0x00007F9D
		internal static HandleRef getCPtr(GameServerGlobalLobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00009DBC File Offset: 0x00007FBC
		~GameServerGlobalLobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00009DEC File Offset: 0x00007FEC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyEnteredListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000073 RID: 115
		private HandleRef swigCPtr;
	}
}
