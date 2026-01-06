using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000088 RID: 136
	public abstract class GlobalLobbyEnteredListener : ILobbyEnteredListener
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00011D48 File Offset: 0x0000FF48
		internal GlobalLobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalLobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00011D64 File Offset: 0x0000FF64
		public GlobalLobbyEnteredListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00011D86 File Offset: 0x0000FF86
		internal static HandleRef getCPtr(GlobalLobbyEnteredListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		~GlobalLobbyEnteredListener()
		{
			this.Dispose();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyEntered.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalLobbyEnteredListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000A2 RID: 162
		private HandleRef swigCPtr;
	}
}
