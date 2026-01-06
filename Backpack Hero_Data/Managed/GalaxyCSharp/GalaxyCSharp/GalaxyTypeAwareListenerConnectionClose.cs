using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000022 RID: 34
	public abstract class GalaxyTypeAwareListenerConnectionClose : IGalaxyListener
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00003EFC File Offset: 0x000020FC
		internal GalaxyTypeAwareListenerConnectionClose(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionClose_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00003F18 File Offset: 0x00002118
		public GalaxyTypeAwareListenerConnectionClose()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerConnectionClose(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00003F36 File Offset: 0x00002136
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerConnectionClose obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00003F54 File Offset: 0x00002154
		~GalaxyTypeAwareListenerConnectionClose()
		{
			this.Dispose();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00003F84 File Offset: 0x00002184
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerConnectionClose(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000400C File Offset: 0x0000220C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerConnectionClose_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000036 RID: 54
		private HandleRef swigCPtr;
	}
}
