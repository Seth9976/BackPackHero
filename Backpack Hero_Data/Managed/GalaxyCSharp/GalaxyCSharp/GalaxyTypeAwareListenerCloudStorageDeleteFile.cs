using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001E RID: 30
	public abstract class GalaxyTypeAwareListenerCloudStorageDeleteFile : IGalaxyListener
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00003A2C File Offset: 0x00001C2C
		internal GalaxyTypeAwareListenerCloudStorageDeleteFile(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageDeleteFile_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00003A48 File Offset: 0x00001C48
		public GalaxyTypeAwareListenerCloudStorageDeleteFile()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerCloudStorageDeleteFile(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00003A66 File Offset: 0x00001C66
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerCloudStorageDeleteFile obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00003A84 File Offset: 0x00001C84
		~GalaxyTypeAwareListenerCloudStorageDeleteFile()
		{
			this.Dispose();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerCloudStorageDeleteFile(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00003B3C File Offset: 0x00001D3C
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStorageDeleteFile_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000032 RID: 50
		private HandleRef swigCPtr;
	}
}
