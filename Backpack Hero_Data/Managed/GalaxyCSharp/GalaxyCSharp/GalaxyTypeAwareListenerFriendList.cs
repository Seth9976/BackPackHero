using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200002D RID: 45
	public abstract class GalaxyTypeAwareListenerFriendList : IGalaxyListener
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x00004C38 File Offset: 0x00002E38
		internal GalaxyTypeAwareListenerFriendList(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendList_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00004C54 File Offset: 0x00002E54
		public GalaxyTypeAwareListenerFriendList()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendList(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00004C72 File Offset: 0x00002E72
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendList obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00004C90 File Offset: 0x00002E90
		~GalaxyTypeAwareListenerFriendList()
		{
			this.Dispose();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendList(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00004D48 File Offset: 0x00002F48
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendList_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000041 RID: 65
		private HandleRef swigCPtr;
	}
}
