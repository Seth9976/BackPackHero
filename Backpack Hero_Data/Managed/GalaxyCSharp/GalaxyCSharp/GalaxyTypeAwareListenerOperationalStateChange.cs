using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000044 RID: 68
	public abstract class GalaxyTypeAwareListenerOperationalStateChange : IGalaxyListener
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x000067E4 File Offset: 0x000049E4
		internal GalaxyTypeAwareListenerOperationalStateChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOperationalStateChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00006800 File Offset: 0x00004A00
		public GalaxyTypeAwareListenerOperationalStateChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOperationalStateChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000681E File Offset: 0x00004A1E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOperationalStateChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000683C File Offset: 0x00004A3C
		~GalaxyTypeAwareListenerOperationalStateChange()
		{
			this.Dispose();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000686C File Offset: 0x00004A6C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOperationalStateChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000068F4 File Offset: 0x00004AF4
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOperationalStateChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000058 RID: 88
		private HandleRef swigCPtr;
	}
}
