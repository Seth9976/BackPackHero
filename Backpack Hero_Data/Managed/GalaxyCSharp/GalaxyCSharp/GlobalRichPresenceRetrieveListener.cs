using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000099 RID: 153
	public abstract class GlobalRichPresenceRetrieveListener : IRichPresenceRetrieveListener
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00014724 File Offset: 0x00012924
		internal GlobalRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00014740 File Offset: 0x00012940
		public GlobalRichPresenceRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00014762 File Offset: 0x00012962
		internal static HandleRef getCPtr(GlobalRichPresenceRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00014780 File Offset: 0x00012980
		~GlobalRichPresenceRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000147B0 File Offset: 0x000129B0
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerRichPresenceRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalRichPresenceRetrieveListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x040000B4 RID: 180
		private HandleRef swigCPtr;
	}
}
