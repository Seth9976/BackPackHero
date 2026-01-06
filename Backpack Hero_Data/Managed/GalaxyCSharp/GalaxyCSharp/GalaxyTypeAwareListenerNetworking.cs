using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000042 RID: 66
	public abstract class GalaxyTypeAwareListenerNetworking : IGalaxyListener
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0000657C File Offset: 0x0000477C
		internal GalaxyTypeAwareListenerNetworking(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNetworking_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00006598 File Offset: 0x00004798
		public GalaxyTypeAwareListenerNetworking()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerNetworking(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000065B6 File Offset: 0x000047B6
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerNetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000065D4 File Offset: 0x000047D4
		~GalaxyTypeAwareListenerNetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00006604 File Offset: 0x00004804
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerNetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000668C File Offset: 0x0000488C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerNetworking_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000056 RID: 86
		private HandleRef swigCPtr;
	}
}
