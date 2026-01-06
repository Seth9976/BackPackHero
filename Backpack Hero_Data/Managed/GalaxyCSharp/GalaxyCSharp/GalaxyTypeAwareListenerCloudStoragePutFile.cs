using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000021 RID: 33
	public abstract class GalaxyTypeAwareListenerCloudStoragePutFile : IGalaxyListener
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal GalaxyTypeAwareListenerCloudStoragePutFile(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStoragePutFile_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public GalaxyTypeAwareListenerCloudStoragePutFile()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerCloudStoragePutFile(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00003E02 File Offset: 0x00002002
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerCloudStoragePutFile obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00003E20 File Offset: 0x00002020
		~GalaxyTypeAwareListenerCloudStoragePutFile()
		{
			this.Dispose();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00003E50 File Offset: 0x00002050
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerCloudStoragePutFile(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerCloudStoragePutFile_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000035 RID: 53
		private HandleRef swigCPtr;
	}
}
