using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000043 RID: 67
	public abstract class GalaxyTypeAwareListenerNotification : IGalaxyListener
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x000066B0 File Offset: 0x000048B0
		internal GalaxyTypeAwareListenerNotification(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNotification_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000066CC File Offset: 0x000048CC
		public GalaxyTypeAwareListenerNotification()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNotification(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000066EA File Offset: 0x000048EA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNotification obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00006708 File Offset: 0x00004908
		~GalaxyTypeAwareListenerNotification()
		{
			this.Dispose();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00006738 File Offset: 0x00004938
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNotification(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000067C0 File Offset: 0x000049C0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNotification_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000057 RID: 87
		private HandleRef swigCPtr;
	}
}
