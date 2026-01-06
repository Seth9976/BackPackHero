using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000089 RID: 137
	public abstract class GlobalLobbyLeftListener : ILobbyLeftListener
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x00011E74 File Offset: 0x00010074
		internal GlobalLobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00011E90 File Offset: 0x00010090
		public GlobalLobbyLeftListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00011EB2 File Offset: 0x000100B2
		internal static HandleRef getCPtr(GlobalLobbyLeftListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00011ED0 File Offset: 0x000100D0
		~GlobalLobbyLeftListener()
		{
			this.Dispose();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00011F00 File Offset: 0x00010100
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyLeft.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyLeftListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A3 RID: 163
		private HandleRef swigCPtr;
	}
}
