using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000066 RID: 102
	public abstract class GameServerGlobalRichPresenceRetrieveListener : IRichPresenceRetrieveListener
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0000B694 File Offset: 0x00009894
		internal GameServerGlobalRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public GameServerGlobalRichPresenceRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000B6D2 File Offset: 0x000098D2
		internal static HandleRef getCPtr(GameServerGlobalRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000B6F0 File Offset: 0x000098F0
		~GameServerGlobalRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000B720 File Offset: 0x00009920
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalRichPresenceRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007A RID: 122
		private HandleRef swigCPtr;
	}
}
