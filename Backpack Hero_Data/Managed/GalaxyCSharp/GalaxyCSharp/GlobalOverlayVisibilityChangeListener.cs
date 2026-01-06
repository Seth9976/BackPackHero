using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000095 RID: 149
	public abstract class GlobalOverlayVisibilityChangeListener : IOverlayVisibilityChangeListener
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00013BC9 File Offset: 0x00011DC9
		internal GlobalOverlayVisibilityChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOverlayVisibilityChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00013BE5 File Offset: 0x00011DE5
		public GlobalOverlayVisibilityChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOverlayVisibilityChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00013C07 File Offset: 0x00011E07
		internal static HandleRef getCPtr(GlobalOverlayVisibilityChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00013C28 File Offset: 0x00011E28
		~GlobalOverlayVisibilityChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00013C58 File Offset: 0x00011E58
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOverlayVisibilityChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOverlayVisibilityChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B0 RID: 176
		private HandleRef swigCPtr;
	}
}
