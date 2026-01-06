using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006E RID: 110
	public abstract class GlobalChatRoomMessagesListener : IChatRoomMessagesListener
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0000CEA5 File Offset: 0x0000B0A5
		internal GlobalChatRoomMessagesListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessagesListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000CEC1 File Offset: 0x0000B0C1
		public GlobalChatRoomMessagesListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessages.GetListenerType(), this);
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000CEE3 File Offset: 0x0000B0E3
		internal static HandleRef getCPtr(GlobalChatRoomMessagesListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000CF04 File Offset: 0x0000B104
		~GlobalChatRoomMessagesListener()
		{
			this.Dispose();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000CF34 File Offset: 0x0000B134
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessages.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessagesListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000083 RID: 131
		private HandleRef swigCPtr;
	}
}
