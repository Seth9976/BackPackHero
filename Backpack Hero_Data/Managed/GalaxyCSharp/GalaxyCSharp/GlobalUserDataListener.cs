using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A0 RID: 160
	public abstract class GlobalUserDataListener : IUserDataListener
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00015C1B File Offset: 0x00013E1B
		internal GlobalUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00015C37 File Offset: 0x00013E37
		public GlobalUserDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserData.GetListenerType(), this);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00015C59 File Offset: 0x00013E59
		internal static HandleRef getCPtr(GlobalUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00015C78 File Offset: 0x00013E78
		~GlobalUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00015CA8 File Offset: 0x00013EA8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BD RID: 189
		private HandleRef swigCPtr;
	}
}
