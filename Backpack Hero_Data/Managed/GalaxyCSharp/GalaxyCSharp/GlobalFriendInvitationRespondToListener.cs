using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007A RID: 122
	public abstract class GlobalFriendInvitationRespondToListener : IFriendInvitationRespondToListener
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0000F845 File Offset: 0x0000DA45
		internal GlobalFriendInvitationRespondToListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationRespondToListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationRespondToListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000F86D File Offset: 0x0000DA6D
		public GlobalFriendInvitationRespondToListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationRespondTo.GetListenerType(), this);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000F88F File Offset: 0x0000DA8F
		internal static HandleRef getCPtr(GlobalFriendInvitationRespondToListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		~GlobalFriendInvitationRespondToListener()
		{
			this.Dispose();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationRespondTo.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationRespondToListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationRespondToListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationRespondToListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000091 RID: 145
		private static Dictionary<IntPtr, GlobalFriendInvitationRespondToListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationRespondToListener>();

		// Token: 0x04000092 RID: 146
		private HandleRef swigCPtr;
	}
}
