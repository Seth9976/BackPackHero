using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000052 RID: 82
	public abstract class GalaxyTypeAwareListenerUserData : IGalaxyListener
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x000078BC File Offset: 0x00005ABC
		internal GalaxyTypeAwareListenerUserData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000078D8 File Offset: 0x00005AD8
		public GalaxyTypeAwareListenerUserData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000078F6 File Offset: 0x00005AF6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00007914 File Offset: 0x00005B14
		~GalaxyTypeAwareListenerUserData()
		{
			this.Dispose();
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00007944 File Offset: 0x00005B44
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000079CC File Offset: 0x00005BCC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000066 RID: 102
		private HandleRef swigCPtr;
	}
}
