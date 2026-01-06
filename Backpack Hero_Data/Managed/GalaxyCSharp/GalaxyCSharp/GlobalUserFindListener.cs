using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A1 RID: 161
	public abstract class GlobalUserFindListener : IUserFindListener
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00015FF9 File Offset: 0x000141F9
		internal GlobalUserFindListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserFindListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00016015 File Offset: 0x00014215
		public GlobalUserFindListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserFind.GetListenerType(), this);
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00016037 File Offset: 0x00014237
		internal static HandleRef getCPtr(GlobalUserFindListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00016058 File Offset: 0x00014258
		~GlobalUserFindListener()
		{
			this.Dispose();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00016088 File Offset: 0x00014288
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserFind.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserFindListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BE RID: 190
		private HandleRef swigCPtr;
	}
}
