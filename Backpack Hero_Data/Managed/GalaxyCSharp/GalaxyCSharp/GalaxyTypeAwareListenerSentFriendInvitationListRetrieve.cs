using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004D RID: 77
	public abstract class GalaxyTypeAwareListenerSentFriendInvitationListRetrieve : IGalaxyListener
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x000072B8 File Offset: 0x000054B8
		internal GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000072D4 File Offset: 0x000054D4
		public GalaxyTypeAwareListenerSentFriendInvitationListRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000072F2 File Offset: 0x000054F2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00007310 File Offset: 0x00005510
		~GalaxyTypeAwareListenerSentFriendInvitationListRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00007340 File Offset: 0x00005540
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000073C8 File Offset: 0x000055C8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000061 RID: 97
		private HandleRef swigCPtr;
	}
}
