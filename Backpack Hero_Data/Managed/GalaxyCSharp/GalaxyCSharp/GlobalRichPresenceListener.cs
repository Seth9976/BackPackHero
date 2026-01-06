using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000098 RID: 152
	public abstract class GlobalRichPresenceListener : IRichPresenceListener
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x000145F5 File Offset: 0x000127F5
		internal GlobalRichPresenceListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00014611 File Offset: 0x00012811
		public GlobalRichPresenceListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresence.GetListenerType(), this);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00014633 File Offset: 0x00012833
		internal static HandleRef getCPtr(GlobalRichPresenceListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00014654 File Offset: 0x00012854
		~GlobalRichPresenceListener()
		{
			this.Dispose();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00014684 File Offset: 0x00012884
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresence.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B3 RID: 179
		private HandleRef swigCPtr;
	}
}
