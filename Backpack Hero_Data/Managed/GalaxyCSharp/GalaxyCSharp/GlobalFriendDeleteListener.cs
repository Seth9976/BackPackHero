using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000077 RID: 119
	public abstract class GlobalFriendDeleteListener : IFriendDeleteListener
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		internal GlobalFriendDeleteListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendDeleteListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public GlobalFriendDeleteListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendDelete.GetListenerType(), this);
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000ED2E File Offset: 0x0000CF2E
		internal static HandleRef getCPtr(GlobalFriendDeleteListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		~GlobalFriendDeleteListener()
		{
			this.Dispose();
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendDelete.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendDeleteListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008C RID: 140
		private HandleRef swigCPtr;
	}
}
