using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000059 RID: 89
	public abstract class GameServerGlobalEncryptedAppTicketListener : IEncryptedAppTicketListener
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0000885E File Offset: 0x00006A5E
		internal GameServerGlobalEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000887A File Offset: 0x00006A7A
		public GameServerGlobalEncryptedAppTicketListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000889C File Offset: 0x00006A9C
		internal static HandleRef getCPtr(GameServerGlobalEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000088BC File Offset: 0x00006ABC
		~GameServerGlobalEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000088EC File Offset: 0x00006AEC
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalEncryptedAppTicketListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006D RID: 109
		private HandleRef swigCPtr;
	}
}
