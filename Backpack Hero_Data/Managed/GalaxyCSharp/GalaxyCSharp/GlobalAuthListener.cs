using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006C RID: 108
	public abstract class GlobalAuthListener : IAuthListener
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0000C760 File Offset: 0x0000A960
		internal GlobalAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000C77C File Offset: 0x0000A97C
		public GlobalAuthListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000C79E File Offset: 0x0000A99E
		internal static HandleRef getCPtr(GlobalAuthListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		~GlobalAuthListener()
		{
			this.Dispose();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAuth.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAuthListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000081 RID: 129
		private HandleRef swigCPtr;
	}
}
