using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000071 RID: 113
	public abstract class GlobalConnectionCloseListener : IConnectionCloseListener
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0000D9B3 File Offset: 0x0000BBB3
		internal GlobalConnectionCloseListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalConnectionCloseListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000D9CF File Offset: 0x0000BBCF
		public GlobalConnectionCloseListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerConnectionClose.GetListenerType(), this);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000D9F1 File Offset: 0x0000BBF1
		internal static HandleRef getCPtr(GlobalConnectionCloseListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000DA10 File Offset: 0x0000BC10
		~GlobalConnectionCloseListener()
		{
			this.Dispose();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerConnectionClose.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalConnectionCloseListener(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x04000086 RID: 134
		private HandleRef swigCPtr;
	}
}
