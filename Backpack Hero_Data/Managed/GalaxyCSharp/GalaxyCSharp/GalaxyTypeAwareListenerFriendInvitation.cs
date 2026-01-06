using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000029 RID: 41
	public abstract class GalaxyTypeAwareListenerFriendInvitation : IGalaxyListener
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x00004768 File Offset: 0x00002968
		internal GalaxyTypeAwareListenerFriendInvitation(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitation_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00004784 File Offset: 0x00002984
		public GalaxyTypeAwareListenerFriendInvitation()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitation(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000047A2 File Offset: 0x000029A2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitation obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000047C0 File Offset: 0x000029C0
		~GalaxyTypeAwareListenerFriendInvitation()
		{
			this.Dispose();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000047F0 File Offset: 0x000029F0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitation(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00004878 File Offset: 0x00002A78
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitation_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400003D RID: 61
		private HandleRef swigCPtr;
	}
}
