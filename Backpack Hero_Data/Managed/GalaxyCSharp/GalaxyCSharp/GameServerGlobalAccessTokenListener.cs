using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000057 RID: 87
	public abstract class GameServerGlobalAccessTokenListener : IAccessTokenListener
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x000080B7 File Offset: 0x000062B7
		internal GameServerGlobalAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000080D3 File Offset: 0x000062D3
		public GameServerGlobalAccessTokenListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000080F5 File Offset: 0x000062F5
		internal static HandleRef getCPtr(GameServerGlobalAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00008114 File Offset: 0x00006314
		~GameServerGlobalAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00008144 File Offset: 0x00006344
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalAccessTokenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006B RID: 107
		private HandleRef swigCPtr;
	}
}
