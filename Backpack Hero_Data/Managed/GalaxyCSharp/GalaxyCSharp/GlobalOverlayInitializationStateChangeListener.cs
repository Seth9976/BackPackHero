using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000094 RID: 148
	public abstract class GlobalOverlayInitializationStateChangeListener : IOverlayInitializationStateChangeListener
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00013839 File Offset: 0x00011A39
		internal GlobalOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GlobalOverlayInitializationStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00013861 File Offset: 0x00011A61
		public GlobalOverlayInitializationStateChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00013883 File Offset: 0x00011A83
		internal static HandleRef getCPtr(GlobalOverlayInitializationStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000138A4 File Offset: 0x00011AA4
		~GlobalOverlayInitializationStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000138D4 File Offset: 0x00011AD4
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOverlayInitializationStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GlobalOverlayInitializationStateChangeListener.listeners.ContainsKey(handle))
					{
						GlobalOverlayInitializationStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000AE RID: 174
		private static Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener>();

		// Token: 0x040000AF RID: 175
		private HandleRef swigCPtr;
	}
}
