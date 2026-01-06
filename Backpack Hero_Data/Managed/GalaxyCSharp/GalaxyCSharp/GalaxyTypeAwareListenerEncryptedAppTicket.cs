using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000025 RID: 37
	public abstract class GalaxyTypeAwareListenerEncryptedAppTicket : IGalaxyListener
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00004298 File Offset: 0x00002498
		internal GalaxyTypeAwareListenerEncryptedAppTicket(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerEncryptedAppTicket_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000042B4 File Offset: 0x000024B4
		public GalaxyTypeAwareListenerEncryptedAppTicket()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerEncryptedAppTicket(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000042D2 File Offset: 0x000024D2
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerEncryptedAppTicket obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000042F0 File Offset: 0x000024F0
		~GalaxyTypeAwareListenerEncryptedAppTicket()
		{
			this.Dispose();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00004320 File Offset: 0x00002520
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerEncryptedAppTicket(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000043A8 File Offset: 0x000025A8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerEncryptedAppTicket_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000039 RID: 57
		private HandleRef swigCPtr;
	}
}
