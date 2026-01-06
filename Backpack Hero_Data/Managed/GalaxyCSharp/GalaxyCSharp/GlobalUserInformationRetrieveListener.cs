using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A2 RID: 162
	public abstract class GlobalUserInformationRetrieveListener : IUserInformationRetrieveListener
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00016128 File Offset: 0x00014328
		internal GlobalUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00016150 File Offset: 0x00014350
		public GlobalUserInformationRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00016172 File Offset: 0x00014372
		internal static HandleRef getCPtr(GlobalUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00016190 File Offset: 0x00014390
		~GlobalUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000161C0 File Offset: 0x000143C0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						GlobalUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000BF RID: 191
		private static Dictionary<IntPtr, GlobalUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, GlobalUserInformationRetrieveListener>();

		// Token: 0x040000C0 RID: 192
		private HandleRef swigCPtr;
	}
}
