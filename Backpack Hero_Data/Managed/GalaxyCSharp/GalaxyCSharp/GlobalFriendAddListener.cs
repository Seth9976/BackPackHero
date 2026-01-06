using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000076 RID: 118
	public abstract class GlobalFriendAddListener : IFriendAddListener
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0000E90F File Offset: 0x0000CB0F
		internal GlobalFriendAddListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendAddListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000E92B File Offset: 0x0000CB2B
		public GlobalFriendAddListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendAdd.GetListenerType(), this);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000E94D File Offset: 0x0000CB4D
		internal static HandleRef getCPtr(GlobalFriendAddListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000E96C File Offset: 0x0000CB6C
		~GlobalFriendAddListener()
		{
			this.Dispose();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendAdd.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendAddListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400008B RID: 139
		private HandleRef swigCPtr;
	}
}
