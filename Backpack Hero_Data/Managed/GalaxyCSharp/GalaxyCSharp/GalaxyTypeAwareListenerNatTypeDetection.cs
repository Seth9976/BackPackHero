using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000041 RID: 65
	public abstract class GalaxyTypeAwareListenerNatTypeDetection : IGalaxyListener
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00006448 File Offset: 0x00004648
		internal GalaxyTypeAwareListenerNatTypeDetection(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNatTypeDetection_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00006464 File Offset: 0x00004664
		public GalaxyTypeAwareListenerNatTypeDetection()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNatTypeDetection(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00006482 File Offset: 0x00004682
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNatTypeDetection obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000064A0 File Offset: 0x000046A0
		~GalaxyTypeAwareListenerNatTypeDetection()
		{
			this.Dispose();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000064D0 File Offset: 0x000046D0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNatTypeDetection(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00006558 File Offset: 0x00004758
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNatTypeDetection_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000055 RID: 85
		private HandleRef swigCPtr;
	}
}
