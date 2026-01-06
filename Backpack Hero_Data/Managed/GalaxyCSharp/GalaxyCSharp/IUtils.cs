using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200018D RID: 397
	public class IUtils : IDisposable
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x0001CB5B File Offset: 0x0001AD5B
		internal IUtils(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0001CB77 File Offset: 0x0001AD77
		internal static HandleRef getCPtr(IUtils obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0001CB98 File Offset: 0x0001AD98
		~IUtils()
		{
			this.Dispose();
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUtils(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0001CC48 File Offset: 0x0001AE48
		public virtual void GetImageSize(uint imageID, ref int width, ref int height)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageSize(this.swigCPtr, imageID, ref width, ref height);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0001CC68 File Offset: 0x0001AE68
		public virtual void GetImageRGBA(uint imageID, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageRGBA(this.swigCPtr, imageID, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0001CC88 File Offset: 0x0001AE88
		public virtual void RegisterForNotification(string type)
		{
			GalaxyInstancePINVOKE.IUtils_RegisterForNotification(this.swigCPtr, type);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public virtual uint GetNotification(ulong notificationID, ref bool consumable, ref byte[] type, uint typeLength, byte[] content, uint contentSize)
		{
			uint num = GalaxyInstancePINVOKE.IUtils_GetNotification(this.swigCPtr, notificationID, ref consumable, type, typeLength, content, contentSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		public virtual void ShowOverlayWithWebPage(string url)
		{
			GalaxyInstancePINVOKE.IUtils_ShowOverlayWithWebPage(this.swigCPtr, url);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0001CCFC File Offset: 0x0001AEFC
		public virtual bool IsOverlayVisible()
		{
			bool flag = GalaxyInstancePINVOKE.IUtils_IsOverlayVisible(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0001CD28 File Offset: 0x0001AF28
		public virtual OverlayState GetOverlayState()
		{
			OverlayState overlayState = (OverlayState)GalaxyInstancePINVOKE.IUtils_GetOverlayState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return overlayState;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0001CD52 File Offset: 0x0001AF52
		public virtual void DisableOverlayPopups(string popupGroup)
		{
			GalaxyInstancePINVOKE.IUtils_DisableOverlayPopups(this.swigCPtr, popupGroup);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0001CD70 File Offset: 0x0001AF70
		public virtual GogServicesConnectionState GetGogServicesConnectionState()
		{
			GogServicesConnectionState gogServicesConnectionState = (GogServicesConnectionState)GalaxyInstancePINVOKE.IUtils_GetGogServicesConnectionState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return gogServicesConnectionState;
		}

		// Token: 0x04000308 RID: 776
		private HandleRef swigCPtr;

		// Token: 0x04000309 RID: 777
		protected bool swigCMemOwn;
	}
}
