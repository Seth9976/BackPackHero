using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000047 RID: 71
	public abstract class GalaxyTypeAwareListenerOverlayVisibilityChange : IGalaxyListener
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x00006B80 File Offset: 0x00004D80
		internal GalaxyTypeAwareListenerOverlayVisibilityChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayVisibilityChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00006B9C File Offset: 0x00004D9C
		public GalaxyTypeAwareListenerOverlayVisibilityChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerOverlayVisibilityChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00006BBA File Offset: 0x00004DBA
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerOverlayVisibilityChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00006BD8 File Offset: 0x00004DD8
		~GalaxyTypeAwareListenerOverlayVisibilityChange()
		{
			this.Dispose();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00006C08 File Offset: 0x00004E08
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerOverlayVisibilityChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00006C90 File Offset: 0x00004E90
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerOverlayVisibilityChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400005B RID: 91
		private HandleRef swigCPtr;
	}
}
