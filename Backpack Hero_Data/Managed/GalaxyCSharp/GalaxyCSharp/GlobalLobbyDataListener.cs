using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000085 RID: 133
	public abstract class GlobalLobbyDataListener : ILobbyDataListener
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x000119C4 File Offset: 0x0000FBC4
		internal GlobalLobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public GlobalLobbyDataListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00011A02 File Offset: 0x0000FC02
		internal static HandleRef getCPtr(GlobalLobbyDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00011A20 File Offset: 0x0000FC20
		~GlobalLobbyDataListener()
		{
			this.Dispose();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00011A50 File Offset: 0x0000FC50
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyData.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyDataListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400009F RID: 159
		private HandleRef swigCPtr;
	}
}
