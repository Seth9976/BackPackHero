using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003B RID: 59
	public abstract class GalaxyTypeAwareListenerLobbyLeft : IGalaxyListener
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00005D10 File Offset: 0x00003F10
		internal GalaxyTypeAwareListenerLobbyLeft(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyLeft_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00005D2C File Offset: 0x00003F2C
		public GalaxyTypeAwareListenerLobbyLeft()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyLeft(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00005D4A File Offset: 0x00003F4A
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyLeft obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00005D68 File Offset: 0x00003F68
		~GalaxyTypeAwareListenerLobbyLeft()
		{
			this.Dispose();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00005D98 File Offset: 0x00003F98
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyLeft(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00005E20 File Offset: 0x00004020
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyLeft_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004F RID: 79
		private HandleRef swigCPtr;
	}
}
