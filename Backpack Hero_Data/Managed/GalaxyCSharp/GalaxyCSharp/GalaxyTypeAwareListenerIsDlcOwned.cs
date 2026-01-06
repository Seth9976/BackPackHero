using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000031 RID: 49
	public abstract class GalaxyTypeAwareListenerIsDlcOwned : IGalaxyListener
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x00005108 File Offset: 0x00003308
		internal GalaxyTypeAwareListenerIsDlcOwned(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerIsDlcOwned_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00005124 File Offset: 0x00003324
		public GalaxyTypeAwareListenerIsDlcOwned()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerIsDlcOwned(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00005142 File Offset: 0x00003342
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerIsDlcOwned obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00005160 File Offset: 0x00003360
		~GalaxyTypeAwareListenerIsDlcOwned()
		{
			this.Dispose();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00005190 File Offset: 0x00003390
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerIsDlcOwned(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00005218 File Offset: 0x00003418
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerIsDlcOwned_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000045 RID: 69
		private HandleRef swigCPtr;
	}
}
