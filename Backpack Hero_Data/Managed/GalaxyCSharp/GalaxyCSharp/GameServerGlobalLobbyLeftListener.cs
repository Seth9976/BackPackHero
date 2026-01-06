using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000060 RID: 96
	public abstract class GameServerGlobalLobbyLeftListener : ILobbyLeftListener
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000A0AF File Offset: 0x000082AF
		internal GameServerGlobalLobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000A0CB File Offset: 0x000082CB
		public GameServerGlobalLobbyLeftListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000A0ED File Offset: 0x000082ED
		internal static HandleRef getCPtr(GameServerGlobalLobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000A10C File Offset: 0x0000830C
		~GameServerGlobalLobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000A13C File Offset: 0x0000833C
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyLeftListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000074 RID: 116
		private HandleRef swigCPtr;
	}
}
