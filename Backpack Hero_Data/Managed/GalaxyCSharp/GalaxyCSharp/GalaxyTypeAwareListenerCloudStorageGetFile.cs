using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001F RID: 31
	public abstract class GalaxyTypeAwareListenerCloudStorageGetFile : IGalaxyListener
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00003B60 File Offset: 0x00001D60
		internal GalaxyTypeAwareListenerCloudStorageGetFile(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFile_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00003B7C File Offset: 0x00001D7C
		public GalaxyTypeAwareListenerCloudStorageGetFile()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerCloudStorageGetFile(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00003B9A File Offset: 0x00001D9A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerCloudStorageGetFile obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00003BB8 File Offset: 0x00001DB8
		~GalaxyTypeAwareListenerCloudStorageGetFile()
		{
			this.Dispose();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerCloudStorageGetFile(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00003C70 File Offset: 0x00001E70
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageGetFile_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000033 RID: 51
		private HandleRef swigCPtr;
	}
}
