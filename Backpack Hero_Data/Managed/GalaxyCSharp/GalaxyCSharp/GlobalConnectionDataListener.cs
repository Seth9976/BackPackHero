using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000072 RID: 114
	public abstract class GlobalConnectionDataListener : IConnectionDataListener
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		internal GlobalConnectionDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000DD0F File Offset: 0x0000BF0F
		public GlobalConnectionDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionData.GetListenerType(), this);
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000DD31 File Offset: 0x0000BF31
		internal static HandleRef getCPtr(GlobalConnectionDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000DD50 File Offset: 0x0000BF50
		~GlobalConnectionDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000087 RID: 135
		private HandleRef swigCPtr;
	}
}
