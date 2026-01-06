using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000019 RID: 25
	public abstract class GalaxyTypeAwareListenerAuth : IGalaxyListener
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00003428 File Offset: 0x00001628
		internal GalaxyTypeAwareListenerAuth(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAuth_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00003444 File Offset: 0x00001644
		public GalaxyTypeAwareListenerAuth()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerAuth(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00003462 File Offset: 0x00001662
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerAuth obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00003480 File Offset: 0x00001680
		~GalaxyTypeAwareListenerAuth()
		{
			this.Dispose();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000034B0 File Offset: 0x000016B0
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerAuth(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00003538 File Offset: 0x00001738
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAuth_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002D RID: 45
		private HandleRef swigCPtr;
	}
}
