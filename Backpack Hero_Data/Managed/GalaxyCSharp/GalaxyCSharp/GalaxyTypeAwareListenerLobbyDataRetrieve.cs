using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000038 RID: 56
	public abstract class GalaxyTypeAwareListenerLobbyDataRetrieve : IGalaxyListener
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00005974 File Offset: 0x00003B74
		internal GalaxyTypeAwareListenerLobbyDataRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00005990 File Offset: 0x00003B90
		public GalaxyTypeAwareListenerLobbyDataRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyDataRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000059AE File Offset: 0x00003BAE
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyDataRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000059CC File Offset: 0x00003BCC
		~GalaxyTypeAwareListenerLobbyDataRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000059FC File Offset: 0x00003BFC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyDataRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00005A84 File Offset: 0x00003C84
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyDataRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004C RID: 76
		private HandleRef swigCPtr;
	}
}
