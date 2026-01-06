using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000070 RID: 112
	public abstract class GlobalChatRoomWithUserRetrieveListener : IChatRoomWithUserRetrieveListener
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0000D671 File Offset: 0x0000B871
		internal GlobalChatRoomWithUserRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomWithUserRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000D68D File Offset: 0x0000B88D
		public GlobalChatRoomWithUserRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomWithUserRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		internal static HandleRef getCPtr(GlobalChatRoomWithUserRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		~GlobalChatRoomWithUserRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000D700 File Offset: 0x0000B900
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomWithUserRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomWithUserRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000085 RID: 133
		private HandleRef swigCPtr;
	}
}
