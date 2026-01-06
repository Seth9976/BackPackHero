using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009B RID: 155
	public abstract class GlobalSentFriendInvitationListRetrieveListener : ISentFriendInvitationListRetrieveListener
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00014EF6 File Offset: 0x000130F6
		internal GlobalSentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalSentFriendInvitationListRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00014F1E File Offset: 0x0001311E
		public GlobalSentFriendInvitationListRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00014F40 File Offset: 0x00013140
		internal static HandleRef getCPtr(GlobalSentFriendInvitationListRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00014F60 File Offset: 0x00013160
		~GlobalSentFriendInvitationListRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00014F90 File Offset: 0x00013190
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSentFriendInvitationListRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalSentFriendInvitationListRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalSentFriendInvitationListRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B7 RID: 183
		private static Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener>();

		// Token: 0x040000B8 RID: 184
		private HandleRef swigCPtr;
	}
}
