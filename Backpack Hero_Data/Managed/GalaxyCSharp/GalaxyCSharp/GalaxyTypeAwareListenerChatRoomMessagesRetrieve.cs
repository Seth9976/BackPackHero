using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001C RID: 28
	public abstract class GalaxyTypeAwareListenerChatRoomMessagesRetrieve : IGalaxyListener
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x000037C4 File Offset: 0x000019C4
		internal GalaxyTypeAwareListenerChatRoomMessagesRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessagesRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000037E0 File Offset: 0x000019E0
		public GalaxyTypeAwareListenerChatRoomMessagesRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerChatRoomMessagesRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000037FE File Offset: 0x000019FE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerChatRoomMessagesRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000381C File Offset: 0x00001A1C
		~GalaxyTypeAwareListenerChatRoomMessagesRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000384C File Offset: 0x00001A4C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerChatRoomMessagesRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000038D4 File Offset: 0x00001AD4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomMessagesRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000030 RID: 48
		private HandleRef swigCPtr;
	}
}
