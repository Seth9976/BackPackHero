using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007C RID: 124
	public abstract class GlobalFriendListListener : IFriendListListener
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x0001003E File Offset: 0x0000E23E
		internal GlobalFriendListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFriendListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001005A File Offset: 0x0000E25A
		public GlobalFriendListListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFriendList.GetListenerType(), this);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001007C File Offset: 0x0000E27C
		internal static HandleRef getCPtr(GlobalFriendListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001009C File Offset: 0x0000E29C
		~GlobalFriendListListener()
		{
			this.Dispose();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000100CC File Offset: 0x0000E2CC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFriendList.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFriendListListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000095 RID: 149
		private HandleRef swigCPtr;
	}
}
