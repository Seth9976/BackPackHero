using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000063 RID: 99
	public abstract class GameServerGlobalLobbyMessageListener : ILobbyMessageListener
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000ABF6 File Offset: 0x00008DF6
		internal GameServerGlobalLobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000AC12 File Offset: 0x00008E12
		public GameServerGlobalLobbyMessageListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000AC34 File Offset: 0x00008E34
		internal static HandleRef getCPtr(GameServerGlobalLobbyMessageListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000AC54 File Offset: 0x00008E54
		~GameServerGlobalLobbyMessageListener()
		{
			this.Dispose();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000AC84 File Offset: 0x00008E84
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMessage.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMessageListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000077 RID: 119
		private HandleRef swigCPtr;
	}
}
