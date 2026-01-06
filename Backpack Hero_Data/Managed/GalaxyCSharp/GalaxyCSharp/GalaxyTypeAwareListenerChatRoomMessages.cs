using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001A RID: 26
	public abstract class GalaxyTypeAwareListenerChatRoomMessages : IGalaxyListener
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0000355C File Offset: 0x0000175C
		internal GalaxyTypeAwareListenerChatRoomMessages(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessages_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00003578 File Offset: 0x00001778
		public GalaxyTypeAwareListenerChatRoomMessages()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerChatRoomMessages(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00003596 File Offset: 0x00001796
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerChatRoomMessages obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000035B4 File Offset: 0x000017B4
		~GalaxyTypeAwareListenerChatRoomMessages()
		{
			this.Dispose();
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000035E4 File Offset: 0x000017E4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerChatRoomMessages(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000366C File Offset: 0x0000186C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessages_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002E RID: 46
		private HandleRef swigCPtr;
	}
}
