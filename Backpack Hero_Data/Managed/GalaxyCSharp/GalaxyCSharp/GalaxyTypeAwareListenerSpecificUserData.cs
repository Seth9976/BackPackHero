using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004F RID: 79
	public abstract class GalaxyTypeAwareListenerSpecificUserData : IGalaxyListener
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00007520 File Offset: 0x00005720
		internal GalaxyTypeAwareListenerSpecificUserData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSpecificUserData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000753C File Offset: 0x0000573C
		public GalaxyTypeAwareListenerSpecificUserData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSpecificUserData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000755A File Offset: 0x0000575A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSpecificUserData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00007578 File Offset: 0x00005778
		~GalaxyTypeAwareListenerSpecificUserData()
		{
			this.Dispose();
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000075A8 File Offset: 0x000057A8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSpecificUserData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00007630 File Offset: 0x00005830
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSpecificUserData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000063 RID: 99
		private HandleRef swigCPtr;
	}
}
