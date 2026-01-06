using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006F RID: 111
	public abstract class GlobalChatRoomMessagesRetrieveListener : IChatRoomMessagesRetrieveListener
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0000D282 File Offset: 0x0000B482
		internal GlobalChatRoomMessagesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessagesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000D29E File Offset: 0x0000B49E
		public GlobalChatRoomMessagesRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessagesRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal static HandleRef getCPtr(GlobalChatRoomMessagesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		~GlobalChatRoomMessagesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000D310 File Offset: 0x0000B510
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessagesRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessagesRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000084 RID: 132
		private HandleRef swigCPtr;
	}
}
