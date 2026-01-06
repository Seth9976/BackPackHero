using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000046 RID: 70
	public abstract class GalaxyTypeAwareListenerOverlayInitializationStateChange : IGalaxyListener
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x00006A4C File Offset: 0x00004C4C
		internal GalaxyTypeAwareListenerOverlayInitializationStateChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayInitializationStateChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00006A68 File Offset: 0x00004C68
		public GalaxyTypeAwareListenerOverlayInitializationStateChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOverlayInitializationStateChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00006A86 File Offset: 0x00004C86
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOverlayInitializationStateChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00006AA4 File Offset: 0x00004CA4
		~GalaxyTypeAwareListenerOverlayInitializationStateChange()
		{
			this.Dispose();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOverlayInitializationStateChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00006B5C File Offset: 0x00004D5C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayInitializationStateChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005A RID: 90
		private HandleRef swigCPtr;
	}
}
