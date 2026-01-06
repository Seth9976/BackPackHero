using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002A RID: 42
	public abstract class GalaxyTypeAwareListenerFriendInvitationListRetrieve : IGalaxyListener
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0000489C File Offset: 0x00002A9C
		internal GalaxyTypeAwareListenerFriendInvitationListRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000048B8 File Offset: 0x00002AB8
		public GalaxyTypeAwareListenerFriendInvitationListRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitationListRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000048D6 File Offset: 0x00002AD6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitationListRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000048F4 File Offset: 0x00002AF4
		~GalaxyTypeAwareListenerFriendInvitationListRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00004924 File Offset: 0x00002B24
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitationListRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000049AC File Offset: 0x00002BAC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003E RID: 62
		private HandleRef swigCPtr;
	}
}
