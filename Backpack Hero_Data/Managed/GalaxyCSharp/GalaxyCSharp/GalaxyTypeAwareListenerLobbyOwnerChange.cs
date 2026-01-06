using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000040 RID: 64
	public abstract class GalaxyTypeAwareListenerLobbyOwnerChange : IGalaxyListener
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00006314 File Offset: 0x00004514
		internal GalaxyTypeAwareListenerLobbyOwnerChange(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyOwnerChange_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00006330 File Offset: 0x00004530
		public GalaxyTypeAwareListenerLobbyOwnerChange()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerLobbyOwnerChange(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000634E File Offset: 0x0000454E
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerLobbyOwnerChange obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000636C File Offset: 0x0000456C
		~GalaxyTypeAwareListenerLobbyOwnerChange()
		{
			this.Dispose();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000639C File Offset: 0x0000459C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerLobbyOwnerChange(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00006424 File Offset: 0x00004624
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerLobbyOwnerChange_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000054 RID: 84
		private HandleRef swigCPtr;
	}
}
