using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000049 RID: 73
	public abstract class GalaxyTypeAwareListenerRichPresence : IGalaxyListener
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00006DE8 File Offset: 0x00004FE8
		internal GalaxyTypeAwareListenerRichPresence(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresence_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00006E04 File Offset: 0x00005004
		public GalaxyTypeAwareListenerRichPresence()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerRichPresence(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00006E22 File Offset: 0x00005022
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerRichPresence obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00006E40 File Offset: 0x00005040
		~GalaxyTypeAwareListenerRichPresence()
		{
			this.Dispose();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00006E70 File Offset: 0x00005070
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerRichPresence(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00006EF8 File Offset: 0x000050F8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerRichPresence_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005D RID: 93
		private HandleRef swigCPtr;
	}
}
