using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006D RID: 109
	public abstract class GlobalChatRoomMessageSendListener : IChatRoomMessageSendListener
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0000CB58 File Offset: 0x0000AD58
		internal GlobalChatRoomMessageSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalChatRoomMessageSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public GlobalChatRoomMessageSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerChatRoomMessageSend.GetListenerType(), this);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000CB96 File Offset: 0x0000AD96
		internal static HandleRef getCPtr(GlobalChatRoomMessageSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		~GlobalChatRoomMessageSendListener()
		{
			this.Dispose();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerChatRoomMessageSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalChatRoomMessageSendListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000082 RID: 130
		private HandleRef swigCPtr;
	}
}
