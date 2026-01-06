using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000097 RID: 151
	public abstract class GlobalRichPresenceChangeListener : IRichPresenceChangeListener
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x000142B2 File Offset: 0x000124B2
		internal GlobalRichPresenceChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000142CE File Offset: 0x000124CE
		public GlobalRichPresenceChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceChange.GetListenerType(), this);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000142F0 File Offset: 0x000124F0
		internal static HandleRef getCPtr(GlobalRichPresenceChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00014310 File Offset: 0x00012510
		~GlobalRichPresenceChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00014340 File Offset: 0x00012540
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceChangeListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B2 RID: 178
		private HandleRef swigCPtr;
	}
}
