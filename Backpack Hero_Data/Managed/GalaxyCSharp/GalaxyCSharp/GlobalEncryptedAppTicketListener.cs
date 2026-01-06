using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000074 RID: 116
	public abstract class GlobalEncryptedAppTicketListener : IEncryptedAppTicketListener
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		internal GlobalEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000E20C File Offset: 0x0000C40C
		public GlobalEncryptedAppTicketListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000E22E File Offset: 0x0000C42E
		internal static HandleRef getCPtr(GlobalEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000E24C File Offset: 0x0000C44C
		~GlobalEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000E27C File Offset: 0x0000C47C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalEncryptedAppTicketListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000089 RID: 137
		private HandleRef swigCPtr;
	}
}
