using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200006A RID: 106
	public abstract class GlobalAccessTokenListener : IAccessTokenListener
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x0000C300 File Offset: 0x0000A500
		internal GlobalAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000C31C File Offset: 0x0000A51C
		public GlobalAccessTokenListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000C33E File Offset: 0x0000A53E
		internal static HandleRef getCPtr(GlobalAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000C35C File Offset: 0x0000A55C
		~GlobalAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000C38C File Offset: 0x0000A58C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAccessToken.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAccessTokenListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007F RID: 127
		private HandleRef swigCPtr;
	}
}
