using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007D RID: 125
	public abstract class GlobalGameInvitationReceivedListener : IGameInvitationReceivedListener
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001038F File Offset: 0x0000E58F
		internal GlobalGameInvitationReceivedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGameInvitationReceivedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalGameInvitationReceivedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000103B7 File Offset: 0x0000E5B7
		public GlobalGameInvitationReceivedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGameInvitationReceived.GetListenerType(), this);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000103D9 File Offset: 0x0000E5D9
		internal static HandleRef getCPtr(GlobalGameInvitationReceivedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000103F8 File Offset: 0x0000E5F8
		~GlobalGameInvitationReceivedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00010428 File Offset: 0x0000E628
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGameInvitationReceived.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGameInvitationReceivedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalGameInvitationReceivedListener.listeners.ContainsKey(handle))
					{
						GlobalGameInvitationReceivedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000096 RID: 150
		private static Dictionary<IntPtr, GlobalGameInvitationReceivedListener> listeners = new Dictionary<IntPtr, GlobalGameInvitationReceivedListener>();

		// Token: 0x04000097 RID: 151
		private HandleRef swigCPtr;
	}
}
