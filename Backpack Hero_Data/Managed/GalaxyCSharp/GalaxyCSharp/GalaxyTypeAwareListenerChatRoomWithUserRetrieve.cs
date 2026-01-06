using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001D RID: 29
	public abstract class GalaxyTypeAwareListenerChatRoomWithUserRetrieve : IGalaxyListener
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal GalaxyTypeAwareListenerChatRoomWithUserRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomWithUserRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00003914 File Offset: 0x00001B14
		public GalaxyTypeAwareListenerChatRoomWithUserRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerChatRoomWithUserRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00003932 File Offset: 0x00001B32
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerChatRoomWithUserRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00003950 File Offset: 0x00001B50
		~GalaxyTypeAwareListenerChatRoomWithUserRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00003980 File Offset: 0x00001B80
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerChatRoomWithUserRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00003A08 File Offset: 0x00001C08
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomWithUserRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000031 RID: 49
		private HandleRef swigCPtr;
	}
}
