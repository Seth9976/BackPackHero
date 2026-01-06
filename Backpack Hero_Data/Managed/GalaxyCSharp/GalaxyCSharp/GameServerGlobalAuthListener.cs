using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000058 RID: 88
	public abstract class GameServerGlobalAuthListener : IAuthListener
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x000084A7 File Offset: 0x000066A7
		internal GameServerGlobalAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000084C3 File Offset: 0x000066C3
		public GameServerGlobalAuthListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000084E5 File Offset: 0x000066E5
		internal static HandleRef getCPtr(GameServerGlobalAuthListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00008504 File Offset: 0x00006704
		~GameServerGlobalAuthListener()
		{
			this.Dispose();
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00008534 File Offset: 0x00006734
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalAuthListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400006C RID: 108
		private HandleRef swigCPtr;
	}
}
