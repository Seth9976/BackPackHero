using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000023 RID: 35
	public abstract class GalaxyTypeAwareListenerConnectionData : IGalaxyListener
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00004030 File Offset: 0x00002230
		internal GalaxyTypeAwareListenerConnectionData(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionData_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000404C File Offset: 0x0000224C
		public GalaxyTypeAwareListenerConnectionData()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerConnectionData(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000406A File Offset: 0x0000226A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerConnectionData obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00004088 File Offset: 0x00002288
		~GalaxyTypeAwareListenerConnectionData()
		{
			this.Dispose();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000040B8 File Offset: 0x000022B8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerConnectionData(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00004140 File Offset: 0x00002340
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionData_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000037 RID: 55
		private HandleRef swigCPtr;
	}
}
