using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000064 RID: 100
	public abstract class GameServerGlobalNetworkingListener : INetworkingListener
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0000AF37 File Offset: 0x00009137
		internal GameServerGlobalNetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalNetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000AF53 File Offset: 0x00009153
		public GameServerGlobalNetworkingListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000AF75 File Offset: 0x00009175
		internal static HandleRef getCPtr(GameServerGlobalNetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000AF94 File Offset: 0x00009194
		~GameServerGlobalNetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000AFC4 File Offset: 0x000091C4
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalNetworkingListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000078 RID: 120
		private HandleRef swigCPtr;
	}
}
