using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200003A RID: 58
	public abstract class GalaxyTypeAwareListenerLobbyEntered : IGalaxyListener
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal GalaxyTypeAwareListenerLobbyEntered(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyEntered_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public GalaxyTypeAwareListenerLobbyEntered()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyEntered(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00005C16 File Offset: 0x00003E16
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyEntered obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00005C34 File Offset: 0x00003E34
		~GalaxyTypeAwareListenerLobbyEntered()
		{
			this.Dispose();
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00005C64 File Offset: 0x00003E64
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyEntered(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00005CEC File Offset: 0x00003EEC
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyEntered_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x0400004E RID: 78
		private HandleRef swigCPtr;
	}
}
