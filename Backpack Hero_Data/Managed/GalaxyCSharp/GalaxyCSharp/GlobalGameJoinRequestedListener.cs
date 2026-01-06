using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200007E RID: 126
	public abstract class GlobalGameJoinRequestedListener : IGameJoinRequestedListener
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0001071F File Offset: 0x0000E91F
		internal GlobalGameJoinRequestedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGameJoinRequestedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001073B File Offset: 0x0000E93B
		public GlobalGameJoinRequestedListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGameJoinRequested.GetListenerType(), this);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001075D File Offset: 0x0000E95D
		internal static HandleRef getCPtr(GlobalGameJoinRequestedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001077C File Offset: 0x0000E97C
		~GlobalGameJoinRequestedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000107AC File Offset: 0x0000E9AC
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGameJoinRequested.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGameJoinRequestedListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000098 RID: 152
		private HandleRef swigCPtr;
	}
}
