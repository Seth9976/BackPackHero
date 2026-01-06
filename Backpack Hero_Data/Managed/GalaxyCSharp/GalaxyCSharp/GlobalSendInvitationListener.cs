using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200009A RID: 154
	public abstract class GlobalSendInvitationListener : ISendInvitationListener
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x00014B1E File Offset: 0x00012D1E
		internal GlobalSendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalSendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00014B46 File Offset: 0x00012D46
		public GlobalSendInvitationListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSendInvitation.GetListenerType(), this);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00014B68 File Offset: 0x00012D68
		internal static HandleRef getCPtr(GlobalSendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00014B88 File Offset: 0x00012D88
		~GlobalSendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSendInvitation.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalSendInvitationListener.listeners.ContainsKey(handle))
					{
						GlobalSendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B5 RID: 181
		private static Dictionary<IntPtr, GlobalSendInvitationListener> listeners = new Dictionary<IntPtr, GlobalSendInvitationListener>();

		// Token: 0x040000B6 RID: 182
		private HandleRef swigCPtr;
	}
}
