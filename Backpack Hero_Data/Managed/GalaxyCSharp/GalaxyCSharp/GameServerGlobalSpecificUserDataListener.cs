using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000067 RID: 103
	public abstract class GameServerGlobalSpecificUserDataListener : ISpecificUserDataListener
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		internal GameServerGlobalSpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalSpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000B9F1 File Offset: 0x00009BF1
		public GameServerGlobalSpecificUserDataListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000BA13 File Offset: 0x00009C13
		internal static HandleRef getCPtr(GameServerGlobalSpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000BA34 File Offset: 0x00009C34
		~GameServerGlobalSpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000BA64 File Offset: 0x00009C64
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerSpecificUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalSpecificUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007B RID: 123
		private HandleRef swigCPtr;
	}
}
