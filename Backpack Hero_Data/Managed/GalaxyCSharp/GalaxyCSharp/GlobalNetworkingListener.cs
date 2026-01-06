using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000090 RID: 144
	public abstract class GlobalNetworkingListener : INetworkingListener
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x00012D68 File Offset: 0x00010F68
		internal GlobalNetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalNetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00012D84 File Offset: 0x00010F84
		public GlobalNetworkingListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00012DA6 File Offset: 0x00010FA6
		internal static HandleRef getCPtr(GlobalNetworkingListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00012DC4 File Offset: 0x00010FC4
		~GlobalNetworkingListener()
		{
			this.Dispose();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00012DF4 File Offset: 0x00010FF4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerNetworking.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalNetworkingListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AA RID: 170
		private HandleRef swigCPtr;
	}
}
