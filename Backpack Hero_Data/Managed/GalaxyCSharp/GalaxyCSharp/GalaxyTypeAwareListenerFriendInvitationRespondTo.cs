using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002B RID: 43
	public abstract class GalaxyTypeAwareListenerFriendInvitationRespondTo : IGalaxyListener
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x000049D0 File Offset: 0x00002BD0
		internal GalaxyTypeAwareListenerFriendInvitationRespondTo(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationRespondTo_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000049EC File Offset: 0x00002BEC
		public GalaxyTypeAwareListenerFriendInvitationRespondTo()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitationRespondTo(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00004A0A File Offset: 0x00002C0A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitationRespondTo obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00004A28 File Offset: 0x00002C28
		~GalaxyTypeAwareListenerFriendInvitationRespondTo()
		{
			this.Dispose();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00004A58 File Offset: 0x00002C58
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitationRespondTo(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationRespondTo_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003F RID: 63
		private HandleRef swigCPtr;
	}
}
