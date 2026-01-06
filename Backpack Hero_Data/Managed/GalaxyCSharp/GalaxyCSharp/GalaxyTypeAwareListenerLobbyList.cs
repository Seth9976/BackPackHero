using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003C RID: 60
	public abstract class GalaxyTypeAwareListenerLobbyList : IGalaxyListener
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00005E44 File Offset: 0x00004044
		internal GalaxyTypeAwareListenerLobbyList(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyList_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00005E60 File Offset: 0x00004060
		public GalaxyTypeAwareListenerLobbyList()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyList(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00005E7E File Offset: 0x0000407E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyList obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00005E9C File Offset: 0x0000409C
		~GalaxyTypeAwareListenerLobbyList()
		{
			this.Dispose();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00005ECC File Offset: 0x000040CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyList(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00005F54 File Offset: 0x00004154
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyList_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000050 RID: 80
		private HandleRef swigCPtr;
	}
}
