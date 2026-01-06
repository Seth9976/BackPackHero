using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007B RID: 123
	public abstract class GlobalFriendInvitationSendListener : IFriendInvitationSendListener
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0000FC68 File Offset: 0x0000DE68
		internal GlobalFriendInvitationSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public GlobalFriendInvitationSendListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationSend.GetListenerType(), this);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
		internal static HandleRef getCPtr(GlobalFriendInvitationSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		~GlobalFriendInvitationSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationSendListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000093 RID: 147
		private static Dictionary<IntPtr, GlobalFriendInvitationSendListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationSendListener>();

		// Token: 0x04000094 RID: 148
		private HandleRef swigCPtr;
	}
}
