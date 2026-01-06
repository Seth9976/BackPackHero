using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000053 RID: 83
	public abstract class GalaxyTypeAwareListenerUserFind : IGalaxyListener
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x000079F0 File Offset: 0x00005BF0
		internal GalaxyTypeAwareListenerUserFind(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00007A0C File Offset: 0x00005C0C
		public GalaxyTypeAwareListenerUserFind()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserFind(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00007A2A File Offset: 0x00005C2A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserFind obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00007A48 File Offset: 0x00005C48
		~GalaxyTypeAwareListenerUserFind()
		{
			this.Dispose();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00007A78 File Offset: 0x00005C78
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserFind(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00007B00 File Offset: 0x00005D00
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000067 RID: 103
		private HandleRef swigCPtr;
	}
}
