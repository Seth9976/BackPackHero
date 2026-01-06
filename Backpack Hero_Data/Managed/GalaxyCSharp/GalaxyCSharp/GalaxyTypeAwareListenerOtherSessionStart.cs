using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000045 RID: 69
	public abstract class GalaxyTypeAwareListenerOtherSessionStart : IGalaxyListener
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00006918 File Offset: 0x00004B18
		internal GalaxyTypeAwareListenerOtherSessionStart(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOtherSessionStart_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00006934 File Offset: 0x00004B34
		public GalaxyTypeAwareListenerOtherSessionStart()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOtherSessionStart(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00006952 File Offset: 0x00004B52
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOtherSessionStart obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00006970 File Offset: 0x00004B70
		~GalaxyTypeAwareListenerOtherSessionStart()
		{
			this.Dispose();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000069A0 File Offset: 0x00004BA0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOtherSessionStart(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00006A28 File Offset: 0x00004C28
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOtherSessionStart_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000059 RID: 89
		private HandleRef swigCPtr;
	}
}
