using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000017 RID: 23
	public abstract class GalaxyTypeAwareListenerAccessToken : IGalaxyListener
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x000031C0 File Offset: 0x000013C0
		internal GalaxyTypeAwareListenerAccessToken(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAccessToken_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000031DC File Offset: 0x000013DC
		public GalaxyTypeAwareListenerAccessToken()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerAccessToken(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000031FA File Offset: 0x000013FA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerAccessToken obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00003218 File Offset: 0x00001418
		~GalaxyTypeAwareListenerAccessToken()
		{
			this.Dispose();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00003248 File Offset: 0x00001448
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerAccessToken(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000032D0 File Offset: 0x000014D0
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerAccessToken_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400002B RID: 43
		private HandleRef swigCPtr;
	}
}
