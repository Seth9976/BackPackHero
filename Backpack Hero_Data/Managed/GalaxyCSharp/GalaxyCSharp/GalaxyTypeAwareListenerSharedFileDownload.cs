using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200004E RID: 78
	public abstract class GalaxyTypeAwareListenerSharedFileDownload : IGalaxyListener
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x000073EC File Offset: 0x000055EC
		internal GalaxyTypeAwareListenerSharedFileDownload(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSharedFileDownload_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00007408 File Offset: 0x00005608
		public GalaxyTypeAwareListenerSharedFileDownload()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerSharedFileDownload(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00007426 File Offset: 0x00005626
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerSharedFileDownload obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00007444 File Offset: 0x00005644
		~GalaxyTypeAwareListenerSharedFileDownload()
		{
			this.Dispose();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00007474 File Offset: 0x00005674
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerSharedFileDownload(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000074FC File Offset: 0x000056FC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerSharedFileDownload_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000062 RID: 98
		private HandleRef swigCPtr;
	}
}
