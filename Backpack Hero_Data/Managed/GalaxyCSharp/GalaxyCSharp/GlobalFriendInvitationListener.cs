using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000078 RID: 120
	public abstract class GlobalFriendInvitationListener : IFriendInvitationListener
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x0000F03F File Offset: 0x0000D23F
		internal GlobalFriendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000F067 File Offset: 0x0000D267
		public GlobalFriendInvitationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitation.GetListenerType(), this);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000F089 File Offset: 0x0000D289
		internal static HandleRef getCPtr(GlobalFriendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		~GlobalFriendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitation.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008D RID: 141
		private static Dictionary<IntPtr, GlobalFriendInvitationListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationListener>();

		// Token: 0x0400008E RID: 142
		private HandleRef swigCPtr;
	}
}
