using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000054 RID: 84
	public abstract class GalaxyTypeAwareListenerUserInformationRetrieve : IGalaxyListener
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00007B24 File Offset: 0x00005D24
		internal GalaxyTypeAwareListenerUserInformationRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserInformationRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00007B40 File Offset: 0x00005D40
		public GalaxyTypeAwareListenerUserInformationRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserInformationRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00007B5E File Offset: 0x00005D5E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserInformationRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00007B7C File Offset: 0x00005D7C
		~GalaxyTypeAwareListenerUserInformationRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00007BAC File Offset: 0x00005DAC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserInformationRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00007C34 File Offset: 0x00005E34
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserInformationRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000068 RID: 104
		private HandleRef swigCPtr;
	}
}
