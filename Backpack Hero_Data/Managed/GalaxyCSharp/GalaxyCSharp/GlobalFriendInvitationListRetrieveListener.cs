using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000079 RID: 121
	public abstract class GlobalFriendInvitationListRetrieveListener : IFriendInvitationListRetrieveListener
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0000F416 File Offset: 0x0000D616
		internal GlobalFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000F43E File Offset: 0x0000D63E
		public GlobalFriendInvitationListRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendInvitationListRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000F460 File Offset: 0x0000D660
		internal static HandleRef getCPtr(GlobalFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000F480 File Offset: 0x0000D680
		~GlobalFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendInvitationListRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008F RID: 143
		private static Dictionary<IntPtr, GlobalFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, GlobalFriendInvitationListRetrieveListener>();

		// Token: 0x04000090 RID: 144
		private HandleRef swigCPtr;
	}
}
